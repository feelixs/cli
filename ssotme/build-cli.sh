#!/bin/bash

# build-cli.sh

ROOT_DIR="$( dirname "$( dirname "${BASH_SOURCE[0]}" )")"

# rm old builds
rm -rf "$ROOT_DIR/dist"
rm -rf "$ROOT_DIR/build"

# substitute setuptools with pyinstaller_setuptools
sed -i '' 's/setuptools/pyinstaller_setuptools/' "$ROOT_DIR/setup.py"

source $1
# run the build executable command
python ./setup.py pyinstaller -- -n ssotme --console --onefile --clean \
                              --add-data "ssotme:ssotme" --hidden-import json

# revert the setup.py change
sed -i '' 's/pyinstaller_setuptools/setuptools/' "$ROOT_DIR/setup.py"

deactivate
