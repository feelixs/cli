from setuptools import setup, find_packages
import json
import os
import platform
import subprocess
import sys
from pathlib import Path
from ssotme.cli import BASE_SUPPORTED_DOTNET


class Installer:
    def __init__(self):
        self.BASE_VERSION = "2024.08.23"  # fallback if package.json is not found
        self.BASE_SUPPORTED_DOTNET = BASE_SUPPORTED_DOTNET
        self._dotnet_executable_path = None

    @property
    def dotnet_executable_path(self):
        if self._dotnet_executable_path is None:
            raise Exception("DotNet executable path was not set!")
        return self._dotnet_executable_path

    @property
    def is_windows(self):
        return platform.system() == "Windows"

    @property
    def is_macos(self):
        return platform.system() == "Darwin"

    @property
    def is_linux(self):
        return platform.system() == "Linux"

    def get_dotnet_executable_path(self):
        """Get the absolute path to the dotnet executable."""
        user_home = os.path.expanduser("~")
        dotnet_path = os.path.join(user_home, ".dotnet", "dotnet")
        # Check if the dotnet executable exists
        if os.path.isfile(dotnet_path) and os.access(dotnet_path, os.X_OK):
            print(f"Found dotnet executable at: {dotnet_path}")
            self._dotnet_executable_path = dotnet_path
        else:
            print("WARN: Dotnet executable not found in ~/.dotnet directory")

    def is_dotnet_version_installed(self, required_version):
        # get all installed dotnet versions
        try:
            result = subprocess.run([self.dotnet_executable_path, "--list-sdks"], check=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
            sdk_versions = result.stdout.decode().splitlines()
            for line in sdk_versions:
                version = line.split(" ")[0]
                if version == required_version:
                    return True
            return False
        except Exception as e:
            print(f"Error checking installed dotnet version - {e}: {str(e)}")
            return False

    def get_package_version(self):
        print("Fetching package version from package.json")
        version = self.BASE_VERSION
        try:
            with open("package.json") as f:
                txt = f.read()
                j = json.loads(txt)
                version = j["version"]
        except FileNotFoundError:
            print("Could not find package.json")
        except json.decoder.JSONDecodeError:
            print("Could not parse package.json")
        except Exception as e:
            print(f"Error {e}: {str(e)}")

        print(f"Package version is '{version}'")
        return version

    def get_supported_dotnet_version(self):
        print("Fetching supported dotnet version from package.json")
        version = self.BASE_SUPPORTED_DOTNET
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

    def build_dotnet_project(self):
        """Build the .NET project with the Release configuration."""
        print("Building .NET project...")

        # Get the directory where setup.py is located
        base_dir = os.path.dirname(os.path.abspath(__file__))
        # Navigate to the directory containing the .sln file
        os.chdir(base_dir)

        # Build the project
        print(f"Run {self.dotnet_executable_path} build SSoTme-OST-CLI.sln -c Release")
        try:
            result = subprocess.run(
                [
                    self.dotnet_executable_path,
                    "build", "SSoTme-OST-CLI.sln",
                    "--configuration", "Release",
                ],
                stdout=subprocess.PIPE,
                stderr=subprocess.PIPE,
                text=True
            )

            if result.returncode != 0:
                print(f"Error building .NET project:\n{result.stderr}\n{result.stdout}")
                return False

            print("Build completed successfully.")
            return True
        except FileNotFoundError:
            print("Warning: .sln file not found, skipping build")
            return True
        except Exception as e:
            print(f"Error during build: {e}")
            return False

    def install_dotnet(self, version: str):
        base_dir = os.path.dirname(os.path.abspath(__file__))
        if self.is_macos:
            print("Installing DotNet for MacOS...")
            try:
                print("brew install wget")
                subprocess.run(["brew", "install", "wget"], check=True)
                print(f"wget", "https://dot.net/v1/dotnet-install.sh", "-P", base_dir)
                subprocess.run(["wget", "https://dot.net/v1/dotnet-install.sh", "-P", base_dir], check=True)
                print("chmod", "+x", os.path.join(base_dir, "dotnet-install.sh"))
                subprocess.run(["chmod", "+x", os.path.join(base_dir, "dotnet-install.sh")], check=True)
                print(os.path.join(base_dir, "dotnet-install.sh"), "--version", version)
                subprocess.run([os.path.join(base_dir, "dotnet-install.sh"), "--version", version], check=True)
            except Exception as e:
                print(f"Error during .NET installation: {e}")
                print("Please install .NET SDK manually from https://dotnet.microsoft.com/download")
                sys.exit(1)

    def get_installed_sdk_versions(self):
        """Get all installed .NET SDK versions."""
        try:
            result = subprocess.run([self.dotnet_executable_path, "--list-sdks"], check=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
            sdk_versions = []
            for line in result.stdout.decode().splitlines():
                if line.strip():
                    version = line.split(" ")[0]
                    sdk_versions.append(version)
            return sdk_versions
        except Exception as e:
            print(f"Error checking installed dotnet SDKs - {e}: {str(e)}")
            return []

    def save_dotnet_info(self, version):
        """Save the .NET SDK information to a file."""
        user_home = os.path.expanduser("~")
        dotnet_info_dir = os.path.join(user_home, ".ssotme")
        os.makedirs(dotnet_info_dir, exist_ok=True)

        dotnet_info_path = os.path.join(dotnet_info_dir, "dotnet_info.json")
        sdk_versions = self.get_installed_sdk_versions()

        info = {
            "current_version": version,
            "installed_versions": sdk_versions,
            "executable_path": self._dotnet_executable_path
        }

        with open(dotnet_info_path, "w") as f:
            json.dump(info, f, indent=2)

        print(f"Saved .NET SDK information to {dotnet_info_path}")

    def run_installer(self):
        """Run the installation process before setup package"""
        supported_version = self.get_supported_dotnet_version()

        # check if we need to install dotnet
        self.get_dotnet_executable_path()
        if self._dotnet_executable_path is None or not self.is_dotnet_version_installed(supported_version):
            # dotnet isn't installed, or the wrong version is installed
            print(f"DotNet v{supported_version} not installed - installing it now")
            self.install_dotnet(supported_version)
            # verify dotnet installed
            if self.is_dotnet_version_installed(supported_version):
                print(f"DotNet v{supported_version} successfully installed")
                # Save the dotnet information
                self.save_dotnet_info(supported_version)
            else:
                print(f"The dotnet version specified in the package.json ({supported_version}) was not detected in your system.")
                sys.exit(1)
        else:
            print(f"Found existing dotnet v{supported_version} installation")
            # Save the dotnet information
            self.save_dotnet_info(supported_version)

        base_dir = os.path.dirname(os.path.abspath(__file__))
        cli_dir = os.path.join(base_dir, "ssotme")
        global_json_path = os.path.join(cli_dir, "global.json")

        with open(global_json_path, "w") as f:
            f.write(f"""{{"sdk": {{"version": "{supported_version}"}}}}""")
        print(f"Write global.json to {global_json_path} to use version {supported_version}")

        result = subprocess.run(
            [
                self.dotnet_executable_path, "--info",
            ], stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
        print(result.stdout)

        # Build the .NET project
        build_result = self.build_dotnet_project()
        if not build_result:
            print("Failed to build .NET project. Aborting installation.")
            sys.exit(1)

        print("Installation completed successfully!")
        print("You can now use the 'ssotme', 'aicapture', or 'aic' commands from your terminal.")


i = Installer()
i.run_installer()
setup(
    name="ssotme",
    version=i.get_package_version(),
    description="Python wrapper for SSoTme CLI tools",
    author="",
    author_email="",
    license="GNU",
    include_package_data=True,
    packages=["ssotme"],
    package_data={
        "ssotme": ["global.json"],
        "": ["global.json", "Windows/CLI/bin/Release/net7.0/*.dll", "Windows/CLI/bin/Release/net7.0/*.json", 
             "Windows/CLI/bin/Release/net7.0/*.config", "Windows/CLI/bin/Release/net7.0/runtimes/**/*"]
    },
    py_modules=["ssotme.cli"],
    entry_points={
        "console_scripts": [
            "ssotme = ssotme.cli:main",
            "aicapture = ssotme.cli:main",
            "aic = ssotme.cli:main",
        ]
    },
    classifiers=[
        "Development Status :: 4 - Beta",
        "Intended Audience :: Developers",
        "License :: OSI Approved :: GNU General Public License (GPL)",
        "Programming Language :: Python :: 3.7",
        "Programming Language :: Python :: 3.8",
        "Programming Language :: Python :: 3.9",
        "Programming Language :: Python :: 3.10",
        "Programming Language :: Python :: 3.11",
    ],
    python_requires='>=3.7',
    install_requires=[],
)
