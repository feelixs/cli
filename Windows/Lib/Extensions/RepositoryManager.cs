using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static SSoTme.OST.Lib.CLIOptions.SSoTmeCLIHandler;

public static class RepositoryManager
{

    public static string CloneRepositoryUsingCmd(this GitRepo seed, string directoryName)
    {
        if (String.IsNullOrEmpty(directoryName))
        {
            directoryName = seed.ShortName;

            Console.Write($"\nChoose the directory if `{directoryName}` is not right: ");
            var suggestedName = Console.ReadLine();
            directoryName = String.IsNullOrEmpty(suggestedName) ? directoryName : suggestedName;
        }

        try
        {
            directoryName = directoryName ?? Path.GetFileNameWithoutExtension(seed.ShortName);

            // Setting up the process start information
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = $"clone {seed.Url} \"{directoryName}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            // Starting the process
            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.Start();

                // Reading output to console
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();

                process.WaitForExit();

                // Handling results based on process exit code
                if (process.ExitCode == 0)
                {
                    Console.WriteLine("Repository successfully cloned.");
                    Console.WriteLine(output);
                }
                else
                {
                    Console.WriteLine("Failed to clone the repository:");
                    Console.WriteLine(errors);
                    throw new Exception($"Failed to clone the respository: \n{errors}");
                }
            }
            return new FileInfo(directoryName).FullName;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
