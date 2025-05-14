ARTCHITECTURE=$(uname -p)  # detect if arm or intel processor (we need separate installers for each)

INSTALLERDIR="./Windows/macOS_Installer"

INSTALLERNAME="SSoTme-Installer-$ARTCHITECTURE.pkg"

DEV_KEYCHAIN_ID=""

echo "Running build.sh"
echo "=============================="
sudo /bin/bash "$INSTALLERDIR/Scripts/build.sh" $INSTALLERNAME $DEV_KEYCHAIN_ID
