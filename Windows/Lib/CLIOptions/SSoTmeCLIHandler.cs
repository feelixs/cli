using Plossum.CommandLine;
using SassyMQ.Lib.RabbitMQ;
using SassyMQ.SSOTME.Lib.RabbitMQ;
using SassyMQ.SSOTME.Lib.RMQActors;
using SSoTme.OST.Lib.DataClasses;
using SSoTme.OST.Lib.Extensions;
using SSoTme.OST.Lib.SassySDK.Derived;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SSoTme.OST.Lib.CLIOptions
{


    public partial class SSoTmeCLIHandler
    {
        private SSOTMEPayload result;

        public SMQAccountHolder AccountHolder { get; private set; }
        public DMProxy CoordinatorProxy { get; private set; }


        private SSoTmeCLIHandler()
        {
            this.account = "";
            this.waitTimeout = 30000;
            this.parameters = new List<string>();
            this.addSetting = new List<string>();
            this.removeSetting = new List<string>();
        }

        public static int ProcessCommand(string[] args)
        {
            var cliOptions = new SSoTmeCLIHandler();
            cliOptions.args = args;
            return cliOptions.ProcessCommand();
        }

        public static int ProcessCommand(string commandLine)
        {
            var cliOptions = new SSoTmeCLIHandler();
            cliOptions.commandLine = commandLine;
            return cliOptions.ProcessCommand();
        }


        private int ProcessCommand()
        {
            try
            {

                CommandLineParser parser = new CommandLineParser(this);
                if (!String.IsNullOrEmpty(this.commandLine)) parser.Parse(this.commandLine, false);
                else parser.Parse(this.args, false);

                Console.WriteLine("\n**************************************************");
                Console.WriteLine(parser.UsageInfo.GetHeaderAsString(78));
                Console.WriteLine("**************************************************\n");


                if (String.IsNullOrEmpty(this.transpiler))
                {
                    this.transpiler = parser.RemainingArguments.FirstOrDefault().SafeToString();
                    if (this.transpiler.Contains("/"))
                    {
                        this.account = this.transpiler.Substring(0, this.transpiler.IndexOf("/"));
                        this.transpiler = this.transpiler.Substring(this.transpiler.IndexOf("/") + 1);
                    }
                }

                var additionalArgs = parser.RemainingArguments.Skip(1).ToList();
                for (var i = 0; i < additionalArgs.Count; i++)
                {
                    this.parameters.Add(String.Format("param{0}={1}", i + 1, additionalArgs[i]));
                }


                if (this.help)
                {
                    Console.WriteLine(parser.UsageInfo.GetOptionsAsString(78));
                    return 0;
                }
                else if (this.init) SSoTmeProject.Init();
                else if (parser.HasErrors)
                {
                    Console.WriteLine(parser.UsageInfo.GetErrorsAsString(78));
                    return -1;
                }
                else
                {
                    var project = SSoTmeProject.LoadOrFail(new DirectoryInfo(Environment.CurrentDirectory));

                    this.LoadInputFile();

                    var zfsFileSetFile = default(FileSetFile);
                    if (!ReferenceEquals(this.FileSet, null))
                    {
                        zfsFileSetFile = this.FileSet.FileSetFiles.FirstOrDefault(fodFileSetFile => fodFileSetFile.RelativePath.EndsWith(".zfs", StringComparison.OrdinalIgnoreCase));
                    }


                    if (this.describe)
                    {
                        project.Describe();
                    }
                    else if (this.listSettings)
                    {
                        project.ListSettings();
                    }
                    else if (this.addSetting.Any())
                    {
                        foreach (var setting in this.addSetting)
                        {
                            project.AddSetting(setting);
                        }
                        project.Save();
                    }
                    else if (this.removeSetting.Any())
                    {
                        foreach (var setting in this.removeSetting)
                        {
                            project.RemoveSetting(setting);
                        }
                        project.Save();
                    }
                    else if (this.build)
                    {
                        project.Rebuild(Environment.CurrentDirectory);
                    }
                    else if (this.buildAll)
                    {
                        project.Rebuild();
                    }
                    else if (this.clean && !ReferenceEquals(zfsFileSetFile, null))
                    {
                        var zfsFI = new FileInfo(zfsFileSetFile.RelativePath);
                        if (zfsFI.Exists)
                        {
                            var zippedFileSet = File.ReadAllBytes(zfsFI.FullName);
                            zippedFileSet.CleanZippedFileSet();
                            if (!this.preserveZFS) zfsFI.Delete();
                        }
                    }
                    else if (this.clean && !parser.RemainingArguments.Any())
                    {
                        project.Clean(Environment.CurrentDirectory, this.preserveZFS);
                    }
                    else if (this.cleanAll && !parser.RemainingArguments.Any())
                    {
                        project.Clean(this.preserveZFS);
                    }
                    else if (!parser.RemainingArguments.Any() && !this.clean)
                    {
                        Console.WriteLine("Missing argument name of transpiler");
                        return -1;
                    }
                    else
                    {

                        AccountHolder = new SMQAccountHolder();
                        var currentSSoTmeKey = SSOTMEKey.GetSSoTmeKey(this.runAs);

                        AccountHolder.ReplyTo += AccountHolder_ReplyTo;
                        AccountHolder.Init(currentSSoTmeKey.EmailAddress, currentSSoTmeKey.Secret);


                        var waitForCook = Task.Factory.StartNew(() =>
                        {
                            while (ReferenceEquals(result, null)) Thread.Sleep(100);
                        });

                        waitForCook.Wait(this.waitTimeout);

                        if (ReferenceEquals(result, null))
                        {
                            result = AccountHolder.CreatePayload();
                            result.Exception = new TimeoutException("Timed out waiting for cook");
                        }

                        if (!ReferenceEquals(result.Exception, null))
                        {
                            Console.WriteLine("ERROR: " + result.Exception.Message);
                            Console.WriteLine(result.Exception.StackTrace);
                            return -1;
                        }
                        else
                        {
                            var finalResult = 0;

                            if (!ReferenceEquals(result.Transpiler, null))
                            {
                                Console.WriteLine("\n\nTRANSPILER MATCHED: {0}\n\n", result.Transpiler.Name);
                            }

                            if (this.clean) result.CleanFileSet();
                            else
                            {
                                finalResult = result.SaveFileSet(this.skipClean);
                                if (this.install) project.Install(result);
                            }
                            return finalResult;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n********************************\nERROR: {0}\n********************************\n\n", ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("\n\nPress any key to continue...\n");
                Console.ReadKey();
            }
            finally
            {
                Console.WriteLine("\n\n");
                if (!ReferenceEquals(AccountHolder, null)) AccountHolder.Disconnect();
            }

            return 0;
        }

        public string inputFileContents = "";
        public string transpiler = "";
        public string inputFileSetXml;
        public string[] args;
        public string commandLine;

        public FileSet FileSet { get; private set; }

        public void LoadInputFile()
        {
            if (!String.IsNullOrEmpty(this.input))
            {
                var fs = new FileSet();
                var inputFilePatterns = this.input.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var filePattern in inputFilePatterns)
                {
                    this.ImportFile(filePattern, fs);
                }

                if (fs.FileSetFiles.Any()) this.inputFileContents = fs.FileSetFiles.First().FileContents;

                this.inputFileSetXml = fs.ToXml();
                this.FileSet = fs;
            }
        }

        private void ImportFile(string filePattern, FileSet fs)
        {
            var fileNameReplacement = String.Empty;
            if (filePattern.Contains("="))
            {
                fileNameReplacement = filePattern.Substring(0, filePattern.IndexOf("="));
                filePattern = filePattern.Substring(filePattern.IndexOf("=") + 1);
            }
            var di = new DirectoryInfo(Path.Combine(".", Path.GetDirectoryName(filePattern)));
            filePattern = Path.GetFileName(filePattern);
            var matchingFiles = di.GetFiles(filePattern);
            if (!matchingFiles.Any()) Console.WriteLine("No files matched {0} in {1}", filePattern, di.FullName);

            foreach (var fi in matchingFiles)
            {
                if (fi.Exists)
                {
                    var fsf = new FileSetFile();
                    fsf.RelativePath = String.IsNullOrEmpty(fileNameReplacement) ? fi.Name : fileNameReplacement;
                    fsf.FileContents = File.ReadAllText(fi.FullName);
                    fs.FileSetFiles.Add(fsf);
                } else {
                    Console.WriteLine("INPUT Format: {0} did not match any files in {1}", filePattern, di.FullName);

                }
            }
        }

        private void AccountHolder_ReplyTo(object sender, SassyMQ.Lib.RabbitMQ.PayloadEventArgs<SSOTMEPayload> e)
        {
            if (e.Payload.IsLexiconTerm(LexiconTermEnum.accountholder_ping_ssotmecoordinator))
            {
                CoordinatorProxy = new DMProxy(e.Payload.DirectMessageQueue);
                Console.WriteLine("Got ping response");
                var payload = AccountHolder.CreatePayload();
                payload.SaveCLIOptions(this);
                payload.TranspileRequest = new TranspileRequest();
                payload.TranspileRequest.ZippedInputFileSet = this.inputFileContents.ToSingleTextFileFileSetXml("input.txt").Zip();
                payload.CLIInputFileContents = String.Empty;
                AccountHolder.AccountHolderCommandLineTranspile(payload, CoordinatorProxy);
            }
            else if (e.Payload.IsLexiconTerm(LexiconTermEnum.accountholder_commandlinetranspile_ssotmecoordinator) ||
                    (e.Payload.IsLexiconTerm(LexiconTermEnum.accountholder_requesttranspile_ssotmecoordinator)))
            {
                result = e.Payload;
            }
        }
    }
}
