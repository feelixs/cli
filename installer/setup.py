from setuptools import setup, find_packages
import json
import os
import platform
import subprocess
import sys
from pathlib import Path
from deprecation import deprecated
import site


class Installer:
    def __init__(self):
        self.BASE_VERSION = "2024.08.23"  # fallback if package.json is not found
        self.FALLBACK_DOTNET_VERSION = "7.0.410"  # fallback

    @property
    def is_windows(self):
        return platform.system() == "Windows"

    @property
    def is_macos(self):
        return platform.system() == "Darwin"

    @property
    def is_linux(self):
        return platform.system() == "Linux"

    @staticmethod
    def is_dotnet_version_installed(required_version):
        # get all installed dotnet versions
        try:
            result = subprocess.run(["dotnet", "--list-sdks"], check=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
            sdk_versions = result.stdout.decode().splitlines()
            print(f"Currently installed dotnet versions:\n\n{sdk_versions}")
            for line in sdk_versions:
                version = line.split(" ")[0]
                if version == required_version:
                    print(f"Dotnet version {version} is installed")
                    return True
            return False
        except Exception as e:
            print(f"Error checking installed dotnet version - {e}: {str(e)}")
            return False

    @staticmethod
    def check_dotnet_installed():
        try:
            version = subprocess.run(["dotnet", "--version"], check=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
            return version.stdout.decode().strip()
        except (subprocess.SubprocessError, FileNotFoundError):
            return None

    @staticmethod
    def get_dll_path(dotnet_version: str) -> str:
        """Get the appropriate path to the DLL based on the platform."""
        base_dir = os.path.dirname(os.path.abspath(__file__))
        # trim off the final version number (v.x.x -> v.x)
        dotnet_base_version = dotnet_version.split('.')
        dotnet_base_version = dotnet_base_version[0] + '.' + dotnet_base_version[1]
        return os.path.join(base_dir, "Windows", "CLI", "bin", "Release", f"net{dotnet_base_version}", "SSoTme.OST.CLI.dll")

    def get_package_version(self):
        print("Fetching package version from package.json")
        version = self.BASE_VERSION
        try:
            with open("../package.json") as f:
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

    def get_dotnet_version(self):
        print("Fetching supported dotnet version from package.json")
        version = self.FALLBACK_DOTNET_VERSION
        try:
            with open("../package.json") as f:
                txt = f.read()
                j = json.loads(txt)
                version = j["dotnet"]
        except FileNotFoundError:
            print("Could not find package.json")
        except json.decoder.JSONDecodeError:
            print("Could not parse package.json")
        except Exception as e:
            print(f"{e}: {str(e)}")

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
        try:
            result = subprocess.run(
                ["dotnet", "build", "SSoTme-OST-CLI.sln", "--configuration", "Release"],
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

    def create_launcher_script(self, script_name, dotnet_version):
        """Create a launcher script that calls the appropriate dotnet command."""
        dll_path = self.get_dll_path(dotnet_version)

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
        scripts_dir = Path(site.USER_BASE) / "bin" if not self.is_windows else Path(site.USER_BASE) / "Scripts"
        scripts_dir.mkdir(parents=True, exist_ok=True)

        script_path = scripts_dir / script_name
        if self.is_windows:
            script_path = script_path.with_suffix(".py")

        with open(script_path, "w") as f:
            f.write(script_content)

        # Make the script executable on Unix-like systems
        if not self.is_windows:
            os.chmod(script_path, 0o755)

        return script_path

    def install_command_aliases(self, dotnet_version: str):
        """Install command-line aliases as defined in package.json bin section."""
        for command_name in ["ssotme", "aicapture", "aic"]:
            script_path = self.create_launcher_script(command_name, dotnet_version)
            print(f"Created launcher script: {script_path}")

    @deprecated  # bad practice to add to path automatically :/
    def add_dotnet_path(self):
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
        os.environ["PATH"] = f"{dotnet_bin}:{os.environ['PATH']}" # make sure its also in path for this session

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
        dotnet_version = self.get_dotnet_version()
        self.install_dotnet(dotnet_version)

        # verify dotnet installed
        installed_version = self.check_dotnet_installed()
        if installed_version is None:
            print("Error: .NET SDK is not installed or not in PATH.")
            print("Please install .NET SDK from https://dotnet.microsoft.com/download")
            sys.exit(1)
        elif not self.is_dotnet_version_installed(dotnet_version):
            print(f"The dotnet version specified in the package.json ({dotnet_version}) was not detected in your system.")
            sys.exit(1)

        # Build the .NET project
        if not self.build_dotnet_project():
            print("Failed to build .NET project. Aborting installation.")
            sys.exit(1)

        # Install command-line aliases
        self.install_command_aliases(dotnet_version)

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
    py_modules=["setup"],
    entry_points={
        'console_scripts': [
            'ssotme=setup:run_installer',
            'aicapture=setup:run_installer',
            'aic=setup:run_installer',
        ],
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
