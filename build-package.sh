ARTCHITECTURE=$(uname -p)  # detect if arm or intel processor (we need separate installers for each)

INSTALLERDIR="./Windows/macOS_Installer"

INSTALLERNAME="unsigned_SSoTme-Installer-$ARTCHITECTURE.pkg"
SIGNED_INSTALLER_NAME="SSoTme-Installer-$ARTCHITECTURE.pkg"

DEV_KEYCHAIN_ID=""

echo "Running build.sh"
echo ""
sudo /bin/bash "$INSTALLERDIR/Scripts/build.sh" $INSTALLERNAME

echo ""
echo "Running packagesign.sh"
echo ""
sudo /bin/bash "$INSTALLERDIR/Scripts/packagesign.sh" "$INSTALLERDIR/bin/$INSTALLERNAME" "$INSTALLERDIR/bin/$SIGNED_INSTALLER_NAME" $DEV_KEYCHAIN_ID
