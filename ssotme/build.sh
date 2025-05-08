# bin/bash

rm -rf /Users/michaelfelix/Documents/GitHub/cli/dist
rm -rf /Users/michaelfelix/Documents/GitHub/cli/build

cd /Users/michaelfelix/Documents/GitHub/cli/

# substitute setuptools with pyinstaller_setuptools
sed -i '' 's/setuptools/pyinstaller_setuptools/' /Users/michaelfelix/Documents/GitHub/cli/setup.py

source ~/.venv-3.12/bin/activate
# run the build executable command
python ./setup.py pyinstaller -- -n ssotme --console --onefile --add-data "ssotme:ssotme" --hidden-import json

# revert the setup.py change
sed -i '' 's/pyinstaller_setuptools/setuptools/' /Users/michaelfelix/Documents/GitHub/cli/setup.py
