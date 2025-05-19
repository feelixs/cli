import sys
import subprocess
import shutil
import json
import os


class CustomException(Exception):
    pass


BASE_SUPPORTED_DOTNET = "7.0.410"


def get_home_ssotme_dir():
    home_dir = os.path.expanduser("~")
    ssotme_dir = os.path.join(home_dir, ".ssotme")
    if not os.path.exists(ssotme_dir):
        os.makedirs(ssotme_dir)

    return home_dir, ssotme_dir


def get_base_version_str(fullstr: str):
    dotnet_base_version = fullstr.split('.')
    return dotnet_base_version[0] + '.' + dotnet_base_version[1]


def get_release_path(dotnet_version: str, base_dir):
    """Get the path to the built project (Windows/CLI/bin/Release/...)"""
    # trim off the final version number (v.x.x -> v.x)
    the_path = os.path.join(base_dir, "Windows", "CLI", "bin", "Release", f"net{get_base_version_str(dotnet_version)}")
    if not os.path.exists(the_path):
        raise FileNotFoundError(f"Could not find {the_path}")
    return the_path


def get_dll_path(dotnet_version: str) -> str:
    """Get the dll path relative to this file (ssotme/cli.py -> ssotme/lib/Windows/CLI/bin/Release/...)"""
    base_dir = os.path.join(os.path.dirname(os.path.abspath(__file__)), "lib")
    the_path = os.path.join(get_release_path(dotnet_version, base_dir), "SSoTme.OST.CLI.dll")
    if not os.path.exists(the_path):
        raise FileNotFoundError(f"Could not find {the_path}")
    return the_path


def get_dotnet_info() -> (str, str):
    """Get the saved .NET SDK information."""
    home, ssotme = get_home_ssotme_dir()
    dotnet_info_path = os.path.join(ssotme, "dotnet_info.json")
    try:
        with open(dotnet_info_path, "r") as f:
            return dotnet_info_path, json.load(f)
    except Exception:
        raise CustomException(f"FATAL: Could not find dotnet_info.json\n"
                              f"If it's been moved or modified, please make sure it's in the correct path\n"
                              f"Or if it was deleted, you can re-run the installer, re-install ssotme from "
                              f"https://github.com/ssotme/cli/releases, or run pip install git+https://github.com/ssotme/cli"
                              f"\n\nError reading dotnet_info.json at {dotnet_info_path}\n")


def get_api_keys():
    home, ssotme = get_home_ssotme_dir()
    api_keys_path = os.path.join(ssotme, "ssotme.key")
    try:
        with open(api_keys_path, "r") as f:
            return json.load(f)
    except FileNotFoundError:
        return None  # nonfatal if the file doesn't exist
    except Exception:
        raise CustomException(f"Could not parse ssotme.key at {api_keys_path}. You may need to delete the file and "
                              f"re-run `ssotme -api ...` to save your API keys again.\n")


def main():
    try:
        info_filepath, dotnet_info = get_dotnet_info()
        if dotnet_info is not None and "executable_path" in dotnet_info and os.path.exists(dotnet_info["executable_path"]):
            dotnet = dotnet_info["executable_path"]
        else:
            # fall back to using 'dotnet' command (not direct path to exe)
            dotnet = shutil.which("dotnet")
            if not dotnet:
                print("dotnet is not installed or not in PATH.")
                raise CustomException("The .NET SDK was not found in your System PATH")

        # Get version from saved info or package.json
        version = BASE_SUPPORTED_DOTNET
        if dotnet_info and "using_version" in dotnet_info:
            version = dotnet_info["using_version"]

        dll_path = get_dll_path(version)

        args = sys.argv[1:]
        code = 0
        if len(args) > 0 and (args[0] == "-info"):
            # print debugging info
            print(f"SSOTME Version: {dotnet_info['ssotme_version']}\n"
                  f"Configured to use .NET SDK {version}\n"
                  f"Configured to use .NET executable: {dotnet}\n"
                  f"Using config file: {info_filepath}\n")

            # print api keys that were configured with `ssotme -api ...` if the key file exists in ~/.ssotme/ssotme.key
            try:
                configured_keys = get_api_keys()
                if configured_keys is not None:
                    keys = ""
                    for key in configured_keys:
                        keys += f"{key}: {configured_keys[key]}\n"
                    print(f"Configured API keys:\n{keys}")
            except Exception as e:
                print(e)

            # verify the dotnet version being used
            result = subprocess.run([dotnet, "--version"], stdout=subprocess.PIPE)
            dotnet_version = result.stdout.decode().strip()
            if dotnet_version != version:
                print(f"WARNING: .NET SDK version does not match .NET executable - configured to use .NET SDK {version}, but `{dotnet} --version` returned {dotnet_version}\n")
        else:
            # run the actual cli
            result = subprocess.run([dotnet, dll_path] + args)
            code = result.returncode
    except CustomException as e:
        print(e)
        code = 1
    except KeyboardInterrupt:
        print("Execution interrupted by user")
        code = 2
    except Exception as e:
        print(f"An unexpected error occurred: {type(e).__name__}\n"
              f"Please report this issue at https://github.com/ssotme/cli/issues\n")
        code = 1

    sys.exit(code)
