from setuptools import setup, find_packages
import json
import os
import platform
import subprocess
import sys
from pathlib import Path
from cli import BASE_SUPPORTED_DOTNET


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

    def add_dotnet_path(self):
        # TODO remove
        # bad practice to add to path automatically :/
        def get_env_folder():
            shell = os.environ.get("SHELL", "")
            home = Path.home()
            if "zsh" in shell:
                return home / ".zshrc"
            elif "bash" in shell:
                # macOS prefers .bash_profile; Linux prefers .bashrc
                if (home / ".bash_profile").exists():
                    return home / ".bash_profile"
                else:
                    return home / ".bashrc"
            else:
                # Fallback
                return home / ".profile"

        profile_path = get_env_folder()
        path_entry = '\n# Added by ssotme installer\nexport PATH="$HOME/.dotnet:$PATH"\n'
        with open(profile_path, "a") as f:
            f.write(path_entry)

        print(f"Added ~/.dotnet to PATH in {profile_path}")
        dotnet_bin = os.path.expanduser("~/.dotnet")
        os.environ["PATH"] = f"{dotnet_bin}:{os.environ['PATH']}"  # make sure its also in path for this session

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

        self.add_dotnet_path()  # todo remove

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
            else:
                print(f"The dotnet version specified in the package.json ({supported_version}) was not detected in your system.")
                sys.exit(1)
        else:
            print(f"Found existing dotnet v{supported_version} installation")

        # specify the target sdk version
        with open("global.json", "w") as f:
            f.write(f"""{{"sdk": {{"version": "{supported_version}"}}}}""")
        print(f"Write global.json to use version {supported_version}")
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
    packages=find_packages(),
    include_package_data=True,
    py_modules=["cli"],
    entry_points={
        "console_scripts": [
            "ssotme = cli:main",
            "aicapture = cli:main",
            "aic = cli:main",
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
