ARTCHITECTURE=$(uname -m)  # detect if arm or intel processor (we need separate installers for each)

INSTALLERDIR="./macOS/Installer"

INSTALLERNAME="SSoTme-Installer-$ARTCHITECTURE.pkg"

DEV_INS_KEYCHAIN_ID="8FAA1706C0D63FA5804FDC97B8B9E5F7595EB62F"
DEV_APP_KEYCHAIN_ID="13C7FD097B736FB5B60F536EDFFF05E110FFE19F"
NOTARYPASS="jvae-tfmy-fmut-gple"
APPLEID="mmh@yellahouse.com"

PYTHON_VENV_PATH="$HOME/.venv-3.12"

echo "Running build.sh"
echo "=============================="
/bin/bash "$INSTALLERDIR/Scripts/build.sh" $INSTALLERNAME $DEV_INS_KEYCHAIN_ID $DEV_APP_KEYCHAIN_ID $APPLEID $NOTARYPASS $PYTHON_VENV_PATH
