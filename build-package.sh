ARTCHITECTURE=$(uname -p)  # detect if arm or intel processor (we need separate installers for each)

INSTALLERDIR="./Windows/macOS_Installer"

INSTALLERNAME="SSoTme-Installer-$ARTCHITECTURE.pkg"

DEV_INS_KEYCHAIN_ID=""
DEV_APP_KEYCHAIN_ID=""
NOTARYPASS=""
APPLEID=""

echo "Running build.sh"
echo "=============================="
/bin/bash "$INSTALLERDIR/Scripts/build.sh" $INSTALLERNAME $DEV_INS_KEYCHAIN_ID $DEV_APP_KEYCHAIN_ID $APPLEID $NOTARYPASS
