import sys
import subprocess
import shutil
import json
import os


BASE_SUPPORTED_DOTNET = "7.0.410"


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
    base_dir = os.path.join(os.path.dirname(os.path.abspath(__file__)))
    dotnet_info_path = os.path.join(base_dir, "dotnet_info.json")
    if os.path.exists(dotnet_info_path):
        try:
            with open(dotnet_info_path, "r") as f:
                return dotnet_info_path, json.load(f)
        except Exception as e:
            print(f"Error reading .NET SDK info: {e}")
    return dotnet_info_path, None


def ensure_global_json(version):
    """Ensure global.json exists in site-packages directory where cli.py is installed."""
    base_dir = os.path.dirname(os.path.abspath(__file__))
    global_json_path = os.path.join(base_dir, "global.json")

    # If global.json doesn't exist or has wrong version, create it
    need_update = False
    if not os.path.exists(global_json_path):
        need_update = True
    else:
        try:
            with open(global_json_path) as f:
                data = json.load(f)
                if not data.get("sdk", {}).get("version") == version:
                    need_update = True
        except Exception:
            need_update = True
    
    if need_update:
        with open(global_json_path, "w") as f:
            f.write(f'{{"sdk": {{"version": "{version}"}}}}')
        print(f"Updated global.json to use .NET SDK version {version}")


def main():
    info_filepath, dotnet_info = get_dotnet_info()
    if dotnet_info is not None and "executable_path" in dotnet_info and os.path.exists(dotnet_info["executable_path"]):
        dotnet = dotnet_info["executable_path"]
        print(f"Using .NET SDK `{dotnet}` from saved configuration: {info_filepath}")
    else:
        # fall back to using 'dotnet' command (not direct path to exe)
        dotnet = shutil.which("dotnet")
        if not dotnet:
            print("dotnet is not installed or not in PATH.")
            sys.exit(1)

    # Get version from saved info or package.json
    version = BASE_SUPPORTED_DOTNET
    if dotnet_info and "using_version" in dotnet_info:
        version = dotnet_info["using_version"]
    
    # Ensure global.json exists with correct version
    ensure_global_json(version)
    
    dll_path = get_dll_path(version)
    args = sys.argv[1:]
    try:
        result = subprocess.run([dotnet, dll_path] + args)
        sys.exit(result.returncode)
    except Exception as e:
        print(f"Execution failed: {e}")
        sys.exit(1)
