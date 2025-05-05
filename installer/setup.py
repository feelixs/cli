from setuptools import setup, find_packages
import json

BASE_VERSION = "2024.08.23"  # fallback if package.json is not found


def get_package_version():
    print("Fetching package version from package.json")
    try:
        with open("../package.json") as f:
            txt = f.read()
            j = json.loads(txt)
            return j["version"]
    except FileNotFoundError:
        print("Could not find package.json")
    except json.decoder.JSONDecodeError:
        print("Could not parse package.json")
    except Exception as e:
        print(f"{e}: {str(e)}")

    return BASE_VERSION


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
