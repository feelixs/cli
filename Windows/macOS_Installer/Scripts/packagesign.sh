#!/bin/bash
# packagesign.sh - Script to sign macOS installer packages with Developer ID certificates
# Usage: ./packagesign.sh <input_package> <output_package> <certificate_id>
# Example: ./packagesign.sh unsigned.pkg signed.pkg "65N8AM5L8K"

set -e

# Check if correct number of arguments is provided
if [ $# -lt 3 ]; then
    echo "Usage: $0 <input_package> <output_package> <certificate_id>"
    echo "Certificate IDs can be retrieved from the Keychain Access app after a valid .p12 certificate file is installed"
    exit 1
fi

INPUT_PACKAGE="$1"
OUTPUT_PACKAGE="$2"
CERTIFICATE_ID="$3"

# Check if input package exists
if [ ! -f "$INPUT_PACKAGE" ]; then
    echo "Error: Input package not found at $INPUT_PACKAGE"
    exit 1
fi

# Sign the package
echo "Signing package: $INPUT_PACKAGE -> $OUTPUT_PACKAGE"
echo "Using certificate ID: $CERTIFICATE_ID"

productsign --sign "$CERTIFICATE_ID" "$INPUT_PACKAGE" "$OUTPUT_PACKAGE"

# Verify the signature
echo "Verifying package signature..."
pkgutil --check-signature "$OUTPUT_PACKAGE"

ASSETS_DIR="$( dirname "$( dirname "${BASH_SOURCE[0]}" )")/Assets"
# set the icon of the product .pkg file
if command -v fileicon >/dev/null 2>&1; then
  sudo fileicon set "$OUTPUT_PACKAGE" "$ASSETS_DIR/Icon.icns"
else
  echo "fileicon was not found, please run: brew install fileicon"
fi

echo "Package signing complete"
