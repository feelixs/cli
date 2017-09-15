using GotDotNet.Exslt;
using SSoTme.OST.Lib.DataClasses;
using SSoTme.OST.Lib.Extensions;
using SSOTME.TestConApp.Root.TranspileHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace SSoTme.Lib.XsltHandling
{
    public class XsltHandler
    {
        private string Xslt;
        private BaseHandler BaseHandler { get; set; }

        public string Xml { get; set; }
        public string FileSetXml { get; private set; }
        public DirectoryInfo RootDirInfo { get; private set; }
        public Dictionary<string, string> Parameters { get; private set; }

        private XsltHandler()
        {
            this.Parameters = new Dictionary<String, String>();
        }

        public XsltHandler(BaseHandler baseHandler)
            : this()
        {
            this.BaseHandler = baseHandler;
            this.RootDirInfo = new DirectoryInfo(Path.Combine("C:\\temp\\", Guid.NewGuid().ToString()));
            this.RootDirInfo.Create();
        }

        public void LoadXmlFromFileInfo(FileInfo inputFI)
        {
            // Find the first argument
            if (!inputFI.Exists) throw new Exception(String.Format("Can't load XML File: '{0}'", inputFI.FullName));
            else this.Xml = File.ReadAllText(inputFI.FullName);
        }

        public void AddParameter(string parameterName, string parameterValue)
        {
            this.Parameters[parameterName] = parameterValue;
        }

        public void WriteOutputToRoot()
        {
            this.FileSetXml.SplitFileSetXml(true, this.RootDirInfo.FullName);
        }

        public string ReadRootFile(string fileName)
        {
            return File.ReadAllText(Path.Combine(this.RootDirInfo.FullName, fileName));
        }

        public void LoadInputFileSetFile(FileSetFile inputFileSetFile)
        {
            var finalPath = Path.Combine(this.RootDirInfo.FullName, Path.GetFileName(inputFileSetFile.RelativePath));
            File.WriteAllText(finalPath, inputFileSetFile.FileContents);
        }


        public void LoadXsltFromResourceFile(string resourceName)
        {
            // object o = 1;
            var xslt = new StreamReader(this.BaseHandler.GetType().Assembly.GetManifestResourceStream(resourceName)).ReadToEnd();
            this.LoadXslt(xslt);
        }

        public void ProcessParameters(List<string> cliParameters)
        {
            foreach (var cliParameter in cliParameters)
            {
                var parts = cliParameter.SqlSafeToString().Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var key = parts.FirstOrDefault();
                var value = parts.Skip(1).FirstOrDefault();
                this.Parameters.Add(key, value);
            }
        }

        public void Execute()
        {
            // Process the xml - and then pass the output to a split file contents...
            this.FileSetXml = String.Empty;

        }

        public void LoadXslt(string xslt)
        {
            this.Xslt = xslt;
        }


        private void SplitFileSet()
        {
            this.FileSetXml.SplitFileSetFile(Environment.CurrentDirectory);
        }


        public void Cook()
        {
            this.Cook(this.RootDirInfo.FullName);
        }

        private void Cook(String rootPath)
        {
            // Processs the xslt file
            this.FileSetXml = this.ProcessXslt(rootPath);
        }

        private string ProcessXslt(String rootPath)
        {
            var xslt = this.Xslt;
            var xml = this.Xml;
            if (String.IsNullOrEmpty(this.Xslt)) throw new Exception("Must load xslt before calling Execute()");
            else if (String.IsNullOrEmpty(this.Xml)) throw new Exception("No xml loaded for xslt to process");
            else
            {
                String fileSet = String.Empty;

                Environment.CurrentDirectory = rootPath;

                ExsltTransform t = PrepareTransformationObject(rootPath, this.Xslt);

                XsltArgumentList al = new XsltArgumentList();
                al.AddParam("dish-root", String.Empty, rootPath);
                al.AddParam("codee-root", String.Empty, rootPath);
                al.AddParam("xml-root", String.Empty, rootPath);
                al.AddParam("xslt-root", String.Empty, rootPath);
                foreach (var dict in this.Parameters)
                {
                    al.AddParam(dict.Key, String.Empty, dict.Value);
                }

                String inputXml = this.Xml;

                String newFileContents = String.Empty;

                XmlDocument doc = new XmlDocument();
                inputXml = inputXml.Trim();
                doc.LoadXml(inputXml);
                MemoryStream ms = new MemoryStream();
                try
                {
                    t.Transform(doc.CreateNavigator(), al, ms);

                    ms.Position = 0;

                    String currentDoc = (new UTF8Encoding(true)).GetString(ms.GetBuffer(), 0, (int)ms.Length);
                    if (currentDoc.StartsWith("<?xml")) currentDoc = currentDoc.Substring(currentDoc.IndexOf("?>") + 2);
                    currentDoc = currentDoc.Trim((char)65279);
                    if (currentDoc.Contains("<"))
                    {
                        currentDoc = currentDoc.Substring(currentDoc.IndexOf("<"));
                    }
                    if (!String.IsNullOrEmpty(currentDoc))
                    {
                        try
                        {
                            doc = new XmlDocument();
                            doc.LoadXml(currentDoc);
                            MemoryStream wms = new MemoryStream();
                            XmlTextWriter writer = new XmlTextWriter(wms, (new UTF8Encoding(true)));
                            writer.Formatting = Formatting.Indented;
                            doc.WriteContentTo(writer);
                            writer.Flush();
                            newFileContents = (new UTF8Encoding(true)).GetString(wms.GetBuffer(), 0, (int)writer.BaseStream.Length).Trim();
                            wms.Close();
                        }
                        catch (Exception ex)
                        {
                            throw ex;  // Shoudl really call kitchen service below
                            //KitchenService.ReportError(ex);
                            // Ignore formatting errors
                            newFileContents = currentDoc;
                        }
                    }
                }
                finally
                {
                    ms.Close();
                }


                return newFileContents;
                /* 
                String outputFileName = String.Format("{0}{1}", this.FileName, ".Output.xml");

                newFileContents = newFileContents.Replace(Environment.NewLine, "\n").Replace("\n", Environment.NewLine).Trim();

                this.PreviousFileSetZipped = newFileContents.Zip();

                this.Save();

                //File.WriteAllText(outputFileName, newFileContents);
                Dish.SplitContents(outputFileName, newFileContents, false, this.FileName);



                return fileSet;
                */
            }
        }

        public ExsltTransform PrepareTransformationObject(String rootPath, string xsltText)
        {
            ExsltTransform xslt = new ExsltTransform();
            MemoryStream ms = new MemoryStream((new UTF8Encoding(true)).GetBytes(xsltText));
            try
            {
                XmlTextReader xmltr = new XmlTextReader(ms);
                xmltr.WhitespaceHandling = WhitespaceHandling.Significant;
                xslt.Load(xmltr, new MyResolver(rootPath, this));
            }
            finally
            {
                ms.Close();
            }
            return xslt;
        }

        private class MyResolver : XmlResolver
        {
            public MyResolver(String rootPath, XsltHandler handler)
            {
                this.ForDish = handler;
                this.RootPath = rootPath;
                //Console.WriteLine("REsolver for: " + rootPath);
            }

            public override System.Net.ICredentials Credentials
            {
                set { throw new NotImplementedException(); }
            }

            public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
            {
                var fi = new FileInfo(this.RootPath.FindClosest(absoluteUri.LocalPath));
                if (fi.Exists) return new FileStream(fi.FullName, FileMode.Open);
                else
                {
                    var contents = this.ForDish.BaseHandler.GetType().Assembly.GetTextResourceContents(fi.Name);
                    if (!String.IsNullOrEmpty(contents))
                    {
                        return new MemoryStream(Encoding.UTF8.GetBytes(contents));
                    }
                    else throw new Exception("Can't find resource: " + fi.Name);
                }
            }

            public string RootPath { get; set; }

            public XsltHandler ForDish { get; set; }
        }
    }
}
