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


def get_dotnet_info():
    """Get the saved .NET SDK information."""
    user_home = os.path.expanduser("~")
    dotnet_info_path = os.path.join(user_home, ".ssotme", "dotnet_info.json")
    
    if os.path.exists(dotnet_info_path):
        try:
            with open(dotnet_info_path, "r") as f:
                return json.load(f)
        except Exception as e:
            print(f"Error reading .NET SDK info: {e}")
    
    return None


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
    dotnet_info = get_dotnet_info()
    if dotnet_info and "executable_path" in dotnet_info and os.path.exists(dotnet_info["executable_path"]):
        dotnet = dotnet_info["executable_path"]
        print(f"Using .NET SDK from saved configuration: {dotnet}")
    else:
        # Fall back to PATH
        dotnet = shutil.which("dotnet")
        if not dotnet:
            print("dotnet is not installed or not in PATH.")
            sys.exit(1)

    # Get version from saved info or package.json
    version = None
    if dotnet_info and "current_version" in dotnet_info:
        version = dotnet_info["current_version"]
    
    if not version:
        version = get_package_dotnet_version()
    
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
