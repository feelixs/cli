using Plossum.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoTme.OST.Lib.CLIOptions
{

    [CommandLineManager(ApplicationName = "AI Capture",
                        Copyright = "Copyright (c) 2022, EJ Alexandra",
                        Description = @"Capturing what AI Knows as Reusable Fishing Poles, rather than just answering your questions (Fish).")]
    public partial class SSoTmeCLIHandler
    {
        
        [CommandLineOption(Description = "Show help about how to use the ssotme cli", MinOccurs = 0, Aliases = "h")]
        public bool help { get; set; }
        
        [CommandLineOption(Description = "Initialize the current folder as the root of an SSOT.me project. An Optional parameter of force will create a sub-project.", MinOccurs = 0, Aliases = "")]
        public bool init { get; set; }
        
        [CommandLineOption(Description = "Saves the current command into the SSoTmeProject file", MinOccurs = 0, Aliases = "")]
        public bool install { get; set; }
        
        [CommandLineOption(Description = "Removes the current command from the SSoTme project file", MinOccurs = 0, Aliases = "")]
        public bool uninstall { get; set; }
        
        [CommandLineOption(Description = "Build any transpilers in the current folder (or children).", MinOccurs = 0, Aliases = "b,replay")]
        public bool build { get; set; }
        
        [CommandLineOption(Description = "Builds all transpilers in the project", MinOccurs = 0, Aliases = "ba,replayall")]
        public bool buildAll { get; set; }
        
        [CommandLineOption(Description = "Describes the current SSoTme project (and all transpilers)", MinOccurs = 0, Aliases = "d")]
        public bool describe { get; set; }
        
        [CommandLineOption(Description = "Descibe all of the transpiler in the project", MinOccurs = 0, Aliases = "da")]
        public bool descibeAll { get; set; }
        
        [CommandLineOption(Description = "Input filename or comma separated list of file names", MinOccurs = 0, Aliases = "i")]
        public List<string> input { get; set; }
        
        [CommandLineOption(Description = "Output filename", MinOccurs = 0, Aliases = "o")]
        public string output { get; set; }
        
        [CommandLineOption(Description = "Don't output the final results - instead, clean", MinOccurs = 0, Aliases = "c")]
        public bool clean { get; set; }
        
        [CommandLineOption(Description = "Don't output the final results - instead, clean", MinOccurs = 0, Aliases = "ca")]
        public bool cleanAll { get; set; }
        
        [CommandLineOption(Description = "Don't clean the output before cooking", MinOccurs = 0, Aliases = "sc")]
        public bool skipClean { get; set; }
        
        [CommandLineOption(Description = "The account which the transpiler belongs to", MinOccurs = 0, Aliases = "a")]
        public string account { get; set; }
        
        [CommandLineOption(Description = "A list of parameters", MinOccurs = 0, Aliases = "p")]
        public List<string> parameters { get; set; }
        
        [CommandLineOption(Description = "List of project settings", MinOccurs = 0, Aliases = "ls")]
        public bool listSettings { get; set; }
        
        [CommandLineOption(Description = "Adds a setting to the SSoTmeProject", MinOccurs = 0, Aliases = "as")]
        public List<string> addSetting { get; set; }
        
        [CommandLineOption(Description = "REmoves a setting from the ssotme project", MinOccurs = 0, Aliases = "rs")]
        public List<string> removeSetting { get; set; }
        
        [CommandLineOption(Description = "The keyfile to use.  By default it looks for ~/.ssotme/ssotme.key. (or ~/.ssotme/ssotme.{username}.key)", MinOccurs = 0, Aliases = "f")]
        public string keyFile { get; set; }
        
        [CommandLineOption(Description = "The email address for the account authenticating", MinOccurs = 0, Aliases = "e")]
        public string emailAddress { get; set; }
        
        [CommandLineOption(Description = "The secret associated with that email address", MinOccurs = 0, Aliases = "k")]
        public string secret { get; set; }
        
        [CommandLineOption(Description = "Add a transpiler to for the given account", MinOccurs = 0, Aliases = "")]
        public string addTranspiler { get; set; }
        
        [CommandLineOption(Description = "Delete the transpiler with the given name", MinOccurs = 0, Aliases = "")]
        public string deleteTranspiler { get; set; }
        
        [CommandLineOption(Description = "The amount of time to wait for the command to continue", MinOccurs = 0, Aliases = "w")]
        public int waitTimeout { get; set; }
        
        [CommandLineOption(Description = "Run as this user (look for this user's key file)", MinOccurs = 0, Aliases = "ra")]
        public string runAs { get; set; }
        
        [CommandLineOption(Description = "Determines if the input should be preserved.", MinOccurs = 0, Aliases = "rz")]
        public bool preserveZFS { get; set; }
        
        [CommandLineOption(Description = "Checks the result of a build linking up input and output files of the transpiles.  Creates a SPXML file in the DSPXml folder of the project.", MinOccurs = 0, Aliases = "cr")]
        public bool checkResults { get; set; }
        
        [CommandLineOption(Description = "Creates documentation based on a DSPXml file created with the -checkResults flag.", MinOccurs = 0, Aliases = "cd")]
        public bool createDocs { get; set; }
        
        [CommandLineOption(Description = "Executes the given command as a ProcessInfo.Start", MinOccurs = 0, Aliases = "exec")]
        public string execute { get; set; }
        
        [CommandLineOption(Description = "Include disabled tools in th ebuild", MinOccurs = 0, Aliases = "id")]
        public bool includeDisabled { get; set; }
        
        [CommandLineOption(Description = "Name of the project (optional parameter to the init command)", MinOccurs = 0, Aliases = "name")]
        public string projectName { get; set; }
        
        [CommandLineOption(Description = "Name of a group to put a transpiler in within a specific folder", MinOccurs = 0, Aliases = "tg")]
        public string transpilerGroup { get; set; }
        
        [CommandLineOption(Description = "Add an account api key", MinOccurs = 0, Aliases = "api")]
        public string setAccountAPIKey { get; set; }
        
        [CommandLineOption(Description = "Send an authentication email to validate user", MinOccurs = 0, Aliases = "auth")]
        public bool authenticate { get; set; }
        
        [CommandLineOption(Description = "Send a registration email to create a new user", MinOccurs = 0, Aliases = "reg")]
        public bool register { get; set; }
        
    }
}
