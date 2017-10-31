# SSoTme-Open-Source-Tools
Open source tools for leveraging the SSoT.me toolset from any 
platform or environment.

## About A Single Source of Truth
Software based on a single source of truth is software that does not tolorate duplication.

*SSoTme* is a command line package manager like NPM, Bower, Nuget, etc - however, the packages
delivered are dynamic in nature.  If 100 projects install a bower package, they all get the
same bytes.

If, by contrast, 100 projects each install the same SSoT.me package, they will all get
different bytes - because they will each be required to provide a single source of truth
which describes their project.  Since each project will start with a different SSoT - 
they will each get a package which works with their project.  It will be the same KIND of 
content that the other projects all needed, but will differ in ways specific to each project.

Similarly - if you one project installs 10 SSoTme packages, they will all match each other,
because they are all derived from the same SSoT.  And any time that SSoT changes, all 10
packages will also update themselves to match the new "truth".

## Open Source Tools
These tools are open source.  Eventually, the SSoT.me Website, Coordinator as well as the Codee42 
and Odxml42 toolsets will also be offered as open source tools as well.  It's just a matter of 
getting them cleaned up a little bit first.

## SSoT.me Architecture
SSoT.me is really a directory of Dynamic Packages (Transpilers).  The distinction between
static package managers like NPM and Bower is that each SSoT.me tool always requires INPUT.
That input is then turned into something else.  By Connecting these tools together end-to-end
a "Transpiler Pipeline" can be created which, in a very dynamic, responsive and flexible way
turns A into B.  The transaction always follows this basic script though:

1. The CLI gathers together the requeseted "input" (files, parameters, options, etc)
2. The CLI packages everything into a "Zipped Json" *Transpile Request*
3. The *Transpile Request* is sent to the SSoT.me coordinator
4. The SSoT.me Coordinator determins which tool is being requested
5. (down the road - The Coordinator Validates subscription and/or charges consumer)
6. The Coordinator forwards the request to a *Transpiler Host* for the requested tool
7. The Transpiler host processes the *Transpile Request*
8. The Transpiler sends the Output directly back to the requesting CLI (the Coordinator is not 
        involved in the response).


## Installation
Installing the Windows *SSoTme.exe* command line tool will automatically update the path
to include the CLI.  

When you register for an account with [SSoT.me](http://ssot.me) - you will be emailed a secret key
That file should be put in this location:

*Key File:* `%USERPROFILE%\Documents\SSoT.me\ssotme.key`
```
{
   "emailAddress": "you@domain.com",
   "secret": "your-secret-key-here-123abc"
}
```

If you have multiple accounts, the format key file should have ethis format: `ssotme.{account-name}.key`
For example: `ssotme.codee42.key`

## Syntax: `ssotme -help`
This command will show the following help.

```
ssotme [account/]transpiler [parameters,...] [options]

options:
   -account, -a          The account which the transpiler belongs to
   -addSetting, -as      Adds a setting to the SSoTmeProject
   -build, -b            Build any transpilers in the
                         current folder (or children).
   -buildAll, -ba        Builds all transpilers in the project
   -checkResults, -cr    Checks the result of a build linking up input
                         and output files of the transpiles.  Creates a
                         SPXML file in the DSPXml folder of the project.
   -clean, -c            Don't output the final results - instead, clean
   -cleanAll, -ca        Don't output the final results - instead, clean
   -createDocs, -cd      Creates documentation based on a DSPXml
                         file created with the -checkResults flag.
   -descibeAll, -da      Descibe all of the transpiler in the project
   -describe, -d         Describes the current SSoTme
                         project (and all transpilers)
   -emailAddress, -e     The email address for the account authenticating
   -execute, -exec       Executes the given command as a ProcessInfo.Start
   -help, -h             Show help about how to use the ssotme cli
   -includeDisabled,
   -id                   Include disabled tools in th ebuild
   -init                 Initialize the current folder as
                         the root of an SSOT.me project
   -input, -i            Input filename or comma separated list of file names
   -install              Saves the current command into the SSoTmeProject file
   -keyFile, -f          The keyfile to use.  By default
                         it looks for ~/SSOT.me/ssotme.key.
   -listSettings, -ls    List of project settings
   -output, -o           Output filename
   -parameters, -p       A list of parameters
   -preserveZFS, -rz     Determines if the input should be preserved.
   -removeSetting, -rs   REmoves a setting from the ssotme project
   -runAs, -ra           Run as this user (look for this user's key file)
   -secret, -k           The secret associated with that email address
   -skipClean, -sc       Don't clean the output before cooking
   -uninstall            Removes the current command
                         from the SSoTme project file
   -waitTimeout, -w      The amount of time to wait
                         for the command to continue
```
