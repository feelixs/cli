using SSoTme.OST.Lib.DataClasses;
using System;
using System.Linq;
using SassyMQ.SSOTME.Lib.RMQActors;
using SSoTme.OST.Lib.Extensions;
using System.Net;
using System.IO;
using SSoTme.Lib.XsltHandling;
using SassyMQ.Lib.RabbitMQ;

namespace SSOTME.TestConApp.Root.TranspileHandlers
{
    public abstract class BaseHandler
    {

        public Transpiler SourceTranspiler;

        public BaseHandler(Transpiler sourceTranspiler, SSOTMEPayload payload)
        {
            this.Payload = payload;
            this.SourceTranspiler = sourceTranspiler;
        }

        public String GetFirstTextFileSetFile()
        {
            if (!this.FileSet.FileSetFiles.Any()) throw new Exception("No input files provided to transpiler.");
            else return this.FileSet.FileSetFiles.First().FileContents;
        }

        public string GetParameter(int parameterIndex)
        {
            var parameterValue = this.Payload
                                     .CLIParams
                                     .Skip(parameterIndex)
                                     .FirstOrDefault()
                                     .SafeToString();
            if (parameterValue.Contains("="))
            {
                parameterValue = parameterValue.Substring(parameterValue.IndexOf("=") + 1);
            }

            return parameterValue;
        }
        public SSOTMEPayload Payload { get; private set; }
        private FileSet _fileSet;
        public FileSet FileSet
        {
            get
            {
                if (ReferenceEquals(_fileSet, null))
                {
                    _fileSet = this.Payload.CLIInputFileSetXml.ToFileSet();
                }
                return _fileSet;
            }
        }

        public byte[] TranspileWithXslt(String inputXml, string xsltPartialName)
        {
            // Transpile a thing
            XsltHandler handler = new XsltHandler(this);
            xsltPartialName = xsltPartialName.ToLower();
            String fileName = this.GetType().Assembly.GetManifestResourceNames().FirstOrDefault(fodResourceName => fodResourceName.ToLower().Contains(xsltPartialName));
            var xslt = new StreamReader(this.GetType().Assembly.GetManifestResourceStream(fileName)).ReadToEnd();

            handler.LoadXslt(xslt);
            handler.ProcessParameters(this.Payload.CLIParams);
            handler.Xml = inputXml;

            handler.Cook();

            return SSOTMEExtensions.Zip(handler.FileSetXml);
        }

        public FileSetFile GetFileSetFileByExtension(string fileExtension, bool isOptionalFile = true)
        {

            var firstMatch = this.FileSet.FileSetFiles.FirstOrDefault(fodFile => String.Equals(Path.GetExtension(fodFile.RelativePath), fileExtension, StringComparison.OrdinalIgnoreCase));
            if (ReferenceEquals(firstMatch, null))
            {
                if (isOptionalFile) return default(FileSetFile);
                else throw new Exception(String.Format("Can't find a file with the extension {0}", fileExtension));
            }
            else return firstMatch;
        }

        public string InputFilePlus(string additionalExtension)
        {
            return String.Format("{0}{1}", this.Payload.CLIInput, additionalExtension);
        }

        public abstract byte[] Transpile(byte[] zippedInputBytes);

        public string FirstString(byte[] zippedInputBytes)
        {
            string firstString = this.GetSingleTextFileContents(zippedInputBytes);
            if (String.IsNullOrEmpty(firstString))
            {
                firstString = this.Payload.CLIParams.FirstOrDefault().StripParamNumber();
            }
            return firstString;
        }

        public string GetSingleTextFileContents(byte[] zippedInputBytes)
        {
            var fileSetXml = zippedInputBytes.UnzipToString();
            if (!String.IsNullOrEmpty(fileSetXml))
            {
                var fileSetFiles = fileSetXml.FileSetFilesFromFileSetXml();
                var firstFile = fileSetFiles.FirstOrDefault();
                if (!ReferenceEquals(firstFile, null)) return firstFile.GetFileContents();
            }

            return String.Empty;
        }
    }
}

