import os
import sys
import subprocess
import platform


def main():
    """Entry point that calls the .NET executable"""
    # Get the directory where this module is installed
    module_dir = os.path.dirname(os.path.abspath(__file__))
    exe_name = "SSoTme.OST.CLI.exe" if platform.system() == "Windows" else "SSoTme.OST.CLI"

    # Find the executable - look for it in the lib directory structure
    # The structure should be: ssotme/lib/Windows/CLI/bin/Release/net{version}/ssotme.exe
    lib_dir = os.path.join(module_dir, "lib", "Windows", "CLI", "bin", "Release")

    exe_path = None
    if os.path.exists(lib_dir):
        # Look for any net*.* directory
        for item in os.listdir(lib_dir):
            if item.startswith("net") and os.path.isdir(os.path.join(lib_dir, item)):
                potential_exe = os.path.join(lib_dir, item, exe_name)
                if os.path.exists(potential_exe):
                    exe_path = potential_exe
                    break

    if not exe_path or not os.path.exists(exe_path):
        print("Error: Could not find SSoTme CLI executable.")
        print(f"Searched in: {lib_dir}")
        print("This may indicate an installation problem.")
        sys.exit(1)

    # Call the executable with all arguments
    try:
        result = subprocess.run(
            [exe_path] + sys.argv[1:],
            stdout=sys.stdout,
            stderr=sys.stderr,
            stdin=sys.stdin
        )
        sys.exit(result.returncode)
    except FileNotFoundError:
        print(f"Error: Could not execute {exe_path}")
        print("The executable may be missing or corrupted.")
        sys.exit(1)
    except Exception as e:
        print(f"Error executing {exe_path}: {e}")
        sys.exit(1)

if __name__ == "__main__":
    main()
