import sys
import subprocess
import shutil
import json
import os


BASE_SUPPORTED_DOTNET = "7.0.410"


def get_package_dotnet_version():
    print("Fetching supported dotnet version from package.json")
    version = BASE_SUPPORTED_DOTNET
    try:
        with open("package.json") as f:
            txt = f.read()
            j = json.loads(txt)
            version = j["dotnet"]
    except FileNotFoundError:
        print("Could not find package.json - using default version")
    except json.decoder.JSONDecodeError:
        print("Could not parse package.json - using default version")
    except Exception as e:
        print(f"Error getting supported version {e}: {str(e)} - using default version")

    print(f"Specified dotnet version is '{version}'")
    return version


def get_dll_path(dotnet_version: str) -> str:
    """Get the appropriate path to the DLL based on the platform."""
    base_dir = os.path.dirname(os.path.abspath(__file__))
    # trim off the final version number (v.x.x -> v.x)
    dotnet_base_version = dotnet_version.split('.')
    dotnet_base_version = dotnet_base_version[0] + '.' + dotnet_base_version[1]
    return os.path.join(base_dir, "Windows", "CLI", "bin", "Release", f"net{dotnet_base_version}", "SSoTme.OST.CLI.dll")


def main():
    dotnet = shutil.which("dotnet")
    if not dotnet:
        print("dotnet is not installed or not in PATH.")
        sys.exit(1)

    dll_path = get_dll_path(get_package_dotnet_version())
    args = sys.argv[1:]
    try:
        result = subprocess.run(["dotnet", dll_path] + args)
        sys.exit(result.returncode)
    except Exception as e:
        print(f"Execution failed: {e}")
        sys.exit(1)
