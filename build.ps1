echo "Before build: $(pwd)"
dotnet build AICapture-OST-CLI.sln --configuration Release
echo "After build, CWD is $(pwd)"