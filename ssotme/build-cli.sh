#!/bin/bash

# build-cli.sh

ROOT_DIR="$( dirname "$( dirname "${BASH_SOURCE[0]}" )")"

# rm old builds
rm -rf "$ROOT_DIR/dist"
rm -rf "$ROOT_DIR/build"

# substitute setuptools with pyinstaller_setuptools
sed -i '' 's/setuptools/pyinstaller_setuptools/' "$ROOT_DIR/setup.py"

# Check if virtual environment path was provided
if [ -n "$1" ]; then
  echo "Activating virtual environment at $1"
  source "$1/bin/activate"
else
  echo "No virtual environment path provided, using system Python"
fi

# Check Python version
if ! python -c "import sys; sys.exit(0 if sys.version_info >= (3,0) else 1)"; then
  echo "ERROR Python version >=3 is necessary"
  exit 1
fi

echo "Installing requirements..."
pip install -r "$ROOT_DIR/requirements.txt"

echo "Building executable with PyInstaller..."
# run the build executable command
python ./setup.py pyinstaller -- -n ssotme --console --onefile --clean \
                              --add-data "ssotme:ssotme" --hidden-import json

# revert the setup.py change
sed -i '' 's/pyinstaller_setuptools/setuptools/' "$ROOT_DIR/setup.py"

# Only deactivate if we activated a virtualenv
if [ -n "$1" ]; then
  deactivate
fi
