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

        directoryName = directoryName ?? Path.GetFileNameWithoutExtension(seed.ShortName);

        try
        {
            // Set up the process start information
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = $"clone {seed.Url} \"{directoryName}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            // Start the process
            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                process.WaitForExit();  // Ensure the cloning completes before continuing

                // Read the output and errors
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();

                if (process.ExitCode == 0)
                {
                    Console.WriteLine("Repository successfully cloned.");
                    Console.WriteLine(output);

                    // Remove the .git folder
                    RemoveGitFolder(directoryName);
                }
                else
                {
                    Console.WriteLine("Failed to clone the repository:");
                    Console.WriteLine(errors);
                    throw new Exception($"Failed to clone the repository: \n{errors}");
                }
            }

            return new FileInfo(directoryName).FullName;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in cloning and removing the .git directory: " + ex.Message, ex);
        }
    }

    private static void RemoveGitFolder(string directoryName)
    {
        string gitFolderPath = Path.Combine(directoryName, ".git");
        if (Directory.Exists(gitFolderPath))
        {
            // Attempt to remove read-only attributes
            RemoveReadOnlyAttributes(gitFolderPath);

            // Now attempt to delete the directory
            Directory.Delete(gitFolderPath, true);
            Console.WriteLine(".git folder removed successfully.");
        }
    }

    private static void RemoveReadOnlyAttributes(string directoryPath)
    {
        var directoryInfo = new DirectoryInfo(directoryPath);
        foreach (var file in directoryInfo.GetFiles("*", SearchOption.AllDirectories))
        {
            file.Attributes = FileAttributes.Normal;
        }
        foreach (var dir in directoryInfo.GetDirectories("*", SearchOption.AllDirectories))
        {
            dir.Attributes = FileAttributes.Normal;
        }
    }

}
