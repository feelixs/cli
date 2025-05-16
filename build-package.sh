ARTCHITECTURE=$(uname -m)  # detect if arm or intel processor (we need separate installers for each)

INSTALLERDIR="./macOS/Installer"

INSTALLERNAME="SSoTme-Installer-$ARTCHITECTURE.pkg"

DEV_INS_KEYCHAIN_ID=""
DEV_APP_KEYCHAIN_ID=""
NOTARYPASS=""
APPLEID=""

PYTHON_VENV_PATH="$HOME/.venv"

echo "Running build.sh"
echo "=============================="
/bin/bash "$INSTALLERDIR/Scripts/build.sh" $INSTALLERNAME $DEV_INS_KEYCHAIN_ID $DEV_APP_KEYCHAIN_ID $APPLEID $NOTARYPASS $PYTHON_VENV_PATH
