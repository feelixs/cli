from setuptools import setup, find_packages
import json

BASE_VERSION = "2024.08.23"  # fallback if package.json is not found
SUPPORTED_DOTNET_VERSION = "7.0.410"


def get_package_version():
    print("Fetching package version from package.json")
    version = BASE_VERSION
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
        print(f"{e}: {str(e)}")

    print(f"Package version is '{version}'")
    return version


setup(
    name="ssotme",
    version=get_package_version(),
    description="Python wrapper for SSoTme CLI tools",
    author="",
    author_email="",
    license="GNU",
    packages=find_packages(),
    include_package_data=True,
    py_modules=["installer"],
    entry_points={
        'console_scripts': [
            'ssotme=installer:main',
            'aicapture=installer:main',
            'aic=installer:main',
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
