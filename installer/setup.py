from setuptools import setup, find_packages

import logging

version = "2024.08.23"  # fallback if package.json is not found

logger = logging.getLogger(__name__)


setup(
    name="ssotme",
    version=version,
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
        "Programming Language :: Python :: 3",
        "Programming Language :: Python :: 3.7",
        "Programming Language :: Python :: 3.8",
        "Programming Language :: Python :: 3.9",
        "Programming Language :: Python :: 3.10",
        "Programming Language :: Python :: 3.11",
    ],
    python_requires='>=3.7',
    install_requires=[],
)
