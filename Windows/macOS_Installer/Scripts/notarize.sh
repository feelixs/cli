#!/bin/bash
# notarize.sh - Script to notarize and staple macOS installer packages
# Usage: ./notarize.sh <package_path> <apple_id> <team_id> [password_keychain_item]
# Example: ./notarize.sh signed-SSoTme-Installer-arm.pkg "email@example.com" "ABCDEF" "APPLE_PASSWORD"

# Check if correct number of arguments is provided
if [ $# -lt 3 ]; then
    echo "Usage: $0 <package_path> <apple_id> <team_id> [password_keychain_item]"
    echo "Example: $0 signed-SSoTme-Installer-arm.pkg \"email@example.com\" \"ABCDEF\" \"APPLE_PASSWORD\""
    echo ""
    echo "Notes:"
    echo "  - <package_path>: Path to the signed package to notarize"
    echo "  - <apple_id>: Your Apple ID email"
    echo "  - <team_id>: Your Apple Developer team ID"
    exit 1
fi

PACKAGE_PATH="$1"
APPLE_ID="$2"
TEAM_ID="$3"
PASSWORD_KEYCHAIN_ITEM="$4"

# Check if package exists
if [ ! -f "$PACKAGE_PATH" ]; then
    echo "Error: Package not found at $PACKAGE_PATH"
    exit 1
fi


# Submit for notarization
echo "Submitting package for notarization..."
echo "Package: $PACKAGE_PATH"
echo "Apple ID: $APPLE_ID"
echo "Team ID: $TEAM_ID"

# Use the app password read from input
NOTARIZE_OUTPUT=$(xcrun notarytool submit "$PACKAGE_PATH" \
    --apple-id "$APPLE_ID" \
    --password "$PASSWORD_KEYCHAIN_ITEM" \
    --team-id "$TEAM_ID" \
    --wait)

NOTARIZATION_STATUS=$?

echo "$NOTARIZE_OUTPUT"

# Check if notarization was successful
if [ $NOTARIZATION_STATUS -ne 0 ]; then
    echo "Error: Notarization submission failed"
    exit 1
fi

# Check if the output contains a successful status
if echo "$NOTARIZE_OUTPUT" | grep -q "status: Accepted"; then
    echo "Notarization successful!"

    # Staple the ticket to the package
    echo "Stapling notarization ticket to package..."
    xcrun stapler staple "$PACKAGE_PATH"
    STAPLER_STATUS=$?

    if [ $STAPLER_STATUS -eq 0 ]; then
        echo "Stapling completed successfully"
        
        # Verify the notarization and stapling
        echo "Verifying notarization and stapling..."
        spctl -a -vvv -t install "$PACKAGE_PATH"
        
        echo "Package is now notarized and ready for distribution"
    else
        echo "Error: Stapling failed"
        exit 1
    fi
else
    echo "Error: Notarization was not accepted. Please check the logs."
    exit 1
fi