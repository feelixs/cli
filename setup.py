from setuptools import setup
import shutil
import json
import os
import platform
import subprocess
import re


class DotNetInstallError(Exception):
    pass


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


class Installer:
    def __init__(self):
        pass

    @property
    def arch(self):
        cpu = platform.processor()
        if "64" in cpu:
            return "x64"
        elif cpu == "arm":
            return "arm64"
        else:
            raise Exception(f"Unknown architecture \"{cpu}\"")

    @property
    def is_windows(self):
        return platform.system() == "Windows"

    @staticmethod
    def get_package_version():
        print("Fetching package version from package.json")
        version = None
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
            print(f"Error {type(e).__name__}: {str(e)}")
        if version is None:
            raise Exception("Unable to parse version from package.json")
        print(f"Package version is '{version}'")
        return version

    @staticmethod
    def get_project_dotnet_version(projfile):
        if not os.path.isfile(projfile):
            raise Exception("ERROR: SSoTme.OST.CLI.csproj file not found, couldn't build project")
        pattern = re.compile(r"<TargetFramework>net(\d+\.\d+)</TargetFramework>")
        try:
            with open(projfile, 'r', encoding='utf-8') as file:
                content = file.read()
            match = pattern.search(content)
            if match:
                return match.group(1)  # Returns the version like '8.0'
            else:
                raise Exception("ERROR: Could not find TargetFramework in project file")
        except FileNotFoundError:
            raise Exception(f"ERROR: Project file {projfile} not found")
        except Exception as e:
            raise Exception(f"ERROR: Failed to read project file: {str(e)}")

    @staticmethod
    def get_project_file(base_dir) -> (str, str):
        # Navigate to the directory containing the .sln file
        os.chdir(base_dir)
        pth = os.path.join(base_dir, "Windows", "CLI", "SSoTme.OST.CLI.csproj")
        if not os.path.isfile(pth):
            raise Exception("ERROR: Could not find SSoTme.OST.CLI.csproj file")
        return pth

    def build_dotnet_project(self, base_dir, input_file) -> bool:
        """Build the .NET project with the Release configuration."""
        print("Building .NET project...")
        if not os.path.isfile(input_file):
            raise Exception("ERROR: SSoTme.OST.CLI.csproj file not found, couldn't build project")

        out_dir = os.path.join(base_dir, "dist")
        print(f"dotnet publish \"{input_file}\" -r win-{self.arch} -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained true")
        try:
            result = subprocess.run(
                [
                    "dotnet", "publish", "\"Windows/CLI/SSoTme.OST.CLI.csproj\"", "-r", f"win-{self.arch}", "-c", "Release", "/p:PublishSingleFile=true",
                    "/p:IncludeNativeLibrariesForSelfExtract=true", "--self-contained", "true", "-o", "dist"
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
        except Exception as e:
            print(f"Error during build: {e}")
            return False

    def run_installer(self):
        """Run the installation process before setup package"""
        try:
            base_dir = os.path.dirname(os.path.abspath(__file__))
            project_file = self.get_project_file(base_dir)
            # build dotnet project
            dotnet_version = self.get_project_dotnet_version(project_file)
            build_result = self.build_dotnet_project(base_dir, project_file)
            if not build_result:
                raise RuntimeError("Failed to build .NET project. Please check the build logs for errors.")

            # the built source is now in cli/Windows/CLI/bin/Release
            try:
                built_proj = get_release_path(dotnet_version, base_dir=os.path.dirname(os.path.abspath(__file__)))
            except FileNotFoundError as e:
                raise RuntimeError(f"Could not find build output: {e}")

            # we need to copy it into cli/ssotme/lib/Windows/Release so that it's part of the
            # ssotme pip package and is copied to site-packages later
            if not os.path.exists(built_proj):
                raise RuntimeError(
                    f"Could not find build output at {built_proj}. "
                    "This may indicate a problem with the .NET build process."
                )

            # create the cli/ssotme/lib/Windows/CLI/bin/Release/net7.0 dir structure
            base_dir = os.path.dirname(os.path.abspath(__file__))
            cli_dir = os.path.join(base_dir, "ssotme")
            os.makedirs(cli_dir, exist_ok=True)
            windows_dir = os.path.join(cli_dir, "lib", "Windows", "CLI", "bin", "Release", f"net{get_base_version_str(dotnet_version)}")
            try:
                os.makedirs(windows_dir, exist_ok=True)
            except Exception as e:
                raise RuntimeError(f"Failed to create directory for build output: {e}")

            # copy
            try:
                shutil.copytree(built_proj, windows_dir, dirs_exist_ok=True)
                print(f"Copied build output from {built_proj} to {windows_dir}")
            except Exception as e:
                raise RuntimeError(f"Failed to copy build output: {e}")
                
            print("Installation completed successfully!")
            print("You can now use the 'ssotme', 'aicapture', or 'aic' commands from your terminal.")
            
        except Exception as e:
            print("\n" + "=" * 60)
            print("INSTALLATION ERROR")
            print("=" * 60)
            print(f"Error: {str(e)}")
            print("\nTroubleshooting steps:")
            print("1. Ensure you have the correct permissions to install packages")
            print("2. Try running the installation with administrative privileges")
            print("3. Check that your system meets the requirements (.NET SDK compatibility)")
            print("4. If the problem persists, please report the issue with the error details above")
            print("=" * 60 + "\n")
            raise


def run_setup():
    try:
        # run installation -> builds the dotnet project and installs the cli to system path
        installer = Installer()
        installer.run_installer()
        
        setup_config = {
            "name": "ssotme",
            "version": installer.get_package_version(),
            "description": "Python wrapper for installing SSoTme CLI tools",
            "author": "SSoTme",
            "author_email": "michael@ssot.me",
            "url": "https://ssot.me",
            "license": "GNU",
            "include_package_data": True,
            "packages": ["ssotme"],
            "package_data": {
                "ssotme/lib": ["lib/**/*"]
            },
            "entry_points": {
                "console_scripts": [
                    "ssotme = ssotme.cli:main",
                    "aicapture = ssotme.cli:main",
                    "aic = ssotme.cli:main",
                ]
            },
            "classifiers": [
                "Development Status :: 4 - Beta",
                "Intended Audience :: Developers",
                "License :: OSI Approved :: GNU General Public License (GPL)",
                "Programming Language :: Python :: 3.7",
                "Programming Language :: Python :: 3.8",
                "Programming Language :: Python :: 3.9",
                "Programming Language :: Python :: 3.10",
                "Programming Language :: Python :: 3.11",
            ],
            "python_requires": '>=3.7',
            "install_requires": [],
        }
        setup(**setup_config)
        
    except Exception as e:
        print("\n" + "=" * 60)
        print("SETUP ERROR")
        print("=" * 60)
        print(f"Error: {str(e)}")
        print("\nThe installation process failed. Please check the error message above.")
        print("If you need assistance, please open an issue at https://github.com/SSoTme/cli/issues/new with the error details.")
        print("=" * 60 + "\n")
        # re-raise the error into pip
        raise


if __name__ == "__main__":
    run_setup()
else:
    # when imported (not ran) we don't want to run the installer
    print("Note: setup.py imported as a module, installation process will run when setup() is called.")
