import os
import platform
import subprocess
import sys
from pathlib import Path
import site

import logging


logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] %(message)s",
)
logger = logging.getLogger(__name__)


def is_windows():
    return platform.system() == "Windows"


def is_macos():
    return platform.system() == "Darwin"


def is_linux():
    return platform.system() == "Linux"


def check_dotnet_installed():
    """Check if dotnet is installed and available in PATH."""
    try:
        subprocess.run(["dotnet", "--version"], check=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
        return True
    except (subprocess.SubprocessError, FileNotFoundError):
        return False


def get_dotnet_version():
    version = subprocess.run(["dotnet", "--version"], check=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    return version.stdout.decode().strip()


def build_dotnet_project():
    """Build the .NET project with the Release configuration."""
    logger.info("Building .NET project...")

    # Get the directory where setup.py is located
    base_dir = os.path.dirname(os.path.abspath(__file__))
    logger.info(f"Base directory: {base_dir}")
    # Navigate to the directory containing the .sln file
    os.chdir(base_dir)

    # Build the project
    result = subprocess.run(
        ["dotnet", "build", "SSoTme-OST-CLI.sln", "--configuration", "Release"],
        stdout=subprocess.PIPE,
        stderr=subprocess.PIPE,
        text=True
    )

    if result.returncode != 0:
        print(f"Error building .NET project: {result.stderr}")
        return False

    print("Build completed successfully.")
    return True


def get_dll_path():
    """Get the appropriate path to the DLL based on the platform."""
    base_dir = os.path.dirname(os.path.abspath(__file__))
    logger.info(f"Build path: {base_dir}")
    if is_windows():
        return os.path.join(base_dir, "Windows", "CLI", "bin", "Release", "net7.0", "SSoTme.OST.CLI.dll")
    elif is_macos():
        return os.path.join(base_dir, "macOS", "CLI", "bin", "Release", "net7.0", "SSoTme.OST.CLI.dll")
    elif is_linux():
        return os.path.join(base_dir, "Linux", "CLI", "bin", "Release", "net7.0", "SSoTme.OST.CLI.dll")
    else:
        raise NotImplementedError(f"Unsupported platform: {platform.system()}")


def create_launcher_script(script_name):
    """Create a launcher script that calls the appropriate dotnet command."""
    dll_path = get_dll_path()

    script_content = f"""#!/usr/bin/env python3
import subprocess
import os
import sys

dll_path = "{dll_path}"

# Forward any command-line arguments to the .NET application
args = sys.argv[1:]
command = ["dotnet", dll_path] + args

# Execute the command
subprocess.run(command)
"""

    # Create the script in a platform-appropriate way
    scripts_dir = Path(site.USER_BASE) / "bin" if not is_windows() else Path(site.USER_BASE) / "Scripts"
    scripts_dir.mkdir(parents=True, exist_ok=True)

    script_path = scripts_dir / script_name
    if is_windows():
        script_path = script_path.with_suffix(".py")

    with open(script_path, "w") as f:
        f.write(script_content)

    # Make the script executable on Unix-like systems
    if not is_windows():
        os.chmod(script_path, 0o755)

    return script_path


def install_command_aliases():
    """Install command-line aliases as defined in package.json bin section."""
    for command_name in ["ssotme", "aicapture", "aic"]:
        script_path = create_launcher_script(command_name)
        print(f"Created launcher script: {script_path}")


def main():
    # Check if dotnet is installed
    if not check_dotnet_installed():
        print("Error: .NET SDK is not installed or not in PATH.")
        print("Please install .NET SDK from https://dotnet.microsoft.com/download")
        sys.exit(1)

    # Build the .NET project
    if not build_dotnet_project():
        print("Failed to build .NET project. Aborting installation.")
        sys.exit(1)

    # Install command-line aliases
    install_command_aliases()

    print("Installation completed successfully!")
    print("You can now use the 'ssotme', 'aicapture', or 'aic' commands from your terminal.")


if __name__ == "__main__":
    main()
