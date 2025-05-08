using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Runtime.InteropServices;

namespace SSoTme.CLI
{
    class Program
    {
        private const string BASE_SUPPORTED_DOTNET = "7.0.410";

        static int Main(string[] args)
        {
            try
            {
                // Get home and .ssotme directories
                var (homePath, ssotmePath) = GetHomeSsotmeDir();
                
                // Read .NET info from configuration
                var (infoFilePath, dotnetInfo) = GetDotnetInfo(ssotmePath);
                
                // Determine dotnet executable path
                string dotnetExePath;
                if (dotnetInfo != null && 
                    dotnetInfo.Value.TryGetProperty("executable_path", out var exePathElement) && 
                    !string.IsNullOrEmpty(exePathElement.GetString()) &&
                    File.Exists(exePathElement.GetString()))
                {
                    dotnetExePath = exePathElement.GetString();
                }
                else
                {
                    // Fall back to finding dotnet in PATH
                    dotnetExePath = FindExecutableInPath("dotnet");
                    if (string.IsNullOrEmpty(dotnetExePath))
                    {
                        Console.WriteLine("dotnet is not installed or not in PATH.");
                        return 1;
                    }
                }

                // Get .NET version from config
                string version = BASE_SUPPORTED_DOTNET;
                if (dotnetInfo != null && dotnetInfo.Value.TryGetProperty("using_version", out var versionElement))
                {
                    version = versionElement.GetString() ?? BASE_SUPPORTED_DOTNET;
                }

                // Find the DLL path
                string dllPath = GetDllPath(version);

                int returnCode = 0;
                
                // Check if user is requesting info
                if (args.Length > 0 && (args[0] == "--info" || args[0] == "-i"))
                {
                    // Print debugging info
                    string ssotmeVersion = GetSsotmeVersion(dotnetInfo);
                    Console.WriteLine($"SSOTME Version: {ssotmeVersion}");
                    Console.WriteLine($"Configured to use .NET SDK {version}");
                    Console.WriteLine($"Configured to use .NET executable: {dotnetExePath}");
                    Console.WriteLine($"Using config file: {infoFilePath}");

                    // Verify the dotnet version being used
                    using (var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = dotnetExePath,
                            Arguments = "--version",
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    })
                    {
                        process.Start();
                        string dotnetVersion = process.StandardOutput.ReadToEnd().Trim();
                        process.WaitForExit();
                        
                        if (dotnetVersion != version)
                        {
                            Console.WriteLine($"WARNING: .NET SDK version does not match .NET executable - configured to use .NET SDK {version}, but `{dotnetExePath} --version` returned {dotnetVersion}");
                        }
                    }
                }
                else
                {
                    // Run the actual CLI
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo(dotnetExePath)
                        {
                            UseShellExecute = false,
                            RedirectStandardOutput = false,
                            RedirectStandardError = false,
                            RedirectStandardInput = false
                        };

                        // Add DLL path and all arguments
                        psi.ArgumentList.Add(dllPath);
                        foreach (var arg in args)
                        {
                            psi.ArgumentList.Add(arg);
                        }

                        using (var process = Process.Start(psi))
                        {
                            process.WaitForExit();
                            returnCode = process.ExitCode;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Execution failed: {e.Message}");
                        returnCode = 1;
                    }
                }

                return returnCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 1;
            }
        }

        private static string GetSsotmeVersion(JsonElement? dotnetInfo)
        {
            if (dotnetInfo != null && dotnetInfo.Value.TryGetProperty("ssotme_version", out var versionElement))
            {
                return versionElement.GetString() ?? "unknown";
            }
            return "unknown";
        }

        private static (string homePath, string ssotmePath) GetHomeSsotmeDir()
        {
            string homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string ssotmePath = Path.Combine(homePath, ".ssotme");
            
            if (!Directory.Exists(ssotmePath))
            {
                Directory.CreateDirectory(ssotmePath);
            }
            
            return (homePath, ssotmePath);
        }

        private static (string filePath, JsonElement?) GetDotnetInfo(string ssotmePath)
        {
            string dotnetInfoPath = Path.Combine(ssotmePath, "dotnet_info.json");
            try
            {
                if (File.Exists(dotnetInfoPath))
                {
                    string jsonContent = File.ReadAllText(dotnetInfoPath);
                    using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                    {
                        return (dotnetInfoPath, doc.RootElement.Clone());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FATAL: Could not read dotnet_info.json: {ex.Message}");
                Console.WriteLine($"If it's been moved or modified, please make sure it's in the correct path");
                Console.WriteLine($"Or if it was deleted, re-install ssotme from https://github.com/ssotme/cli or using pip");
                Console.WriteLine($"Error reading dotnet_info.json at {dotnetInfoPath}");
                throw;
            }
            
            return (dotnetInfoPath, null);
        }

        private static string FindExecutableInPath(string executable)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                executable += ".exe";
            }

            var paths = Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator) ?? Array.Empty<string>();
            foreach (var path in paths)
            {
                var fullPath = Path.Combine(path, executable);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }
            
            return null;
        }

        private static string GetBaseVersionString(string fullVersion)
        {
            var parts = fullVersion.Split('.');
            if (parts.Length >= 2)
            {
                return $"{parts[0]}.{parts[1]}";
            }
            return fullVersion;
        }

        private static string GetDllPath(string dotnetVersion)
        {
            // get the path to the cli wrapper dll - cli/Windows/Installer/CLI/bin/Debug/net7.0/ssotme.ddl
            string exeDir = AppContext.BaseDirectory;
            // move out to cli/Windows/
            string winDir = Path.GetFullPath(Path.Combine(exeDir, "..", "..", "..", "..", ".."));
            // go into cli/Windows/CLI/bin/Release/net7.0/ to the actual dll file
            string dllPath = Path.Combine(winDir, "CLI", "bin", "Release", $"net{GetBaseVersionString(dotnetVersion)}", "SSoTme.OST.CLI.dll");

            if (!File.Exists(dllPath))
            {
                throw new FileNotFoundException($"Could not find SSoTme.OST.CLI.dll at {dllPath}");
            }

            return dllPath;
        }
    }
}