using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using SassyMQ.Lib.RabbitMQ;
using SassyMQ.SSOTME.Lib.RabbitMQ;
using SassyMQ.Lib.RabbitMQ.Payload;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


using SassyMQ.SSOTME.Lib.RMQActors;
using System;
using System.IO;
using SSoTme.OST.Lib.Extensions;
using System.Web;
using System.Xml;
using System.Linq;

namespace SassyMQ.SSOTME.Lib
{
    public static class SSOTMEExtensions
    {
        public static event EventHandler FileWritten;
        private static void OnFileWritten(String fileName)
        {
            if (!ReferenceEquals(FileWritten, null)) FileWritten(fileName, EventArgs.Empty);
        }

        public static void SplitFileSetFile(this String fileSetFileName, String basePath)
        {
            String fileContents = File.ReadAllText(fileSetFileName);
            SplitFileSetXml(fileContents, false, basePath);
        }

        public static void SplitFileSetXml(this string fileSetXml, bool overwriteAll, String basePath)
        {
            if (String.IsNullOrEmpty(fileSetXml) || !fileSetXml.Contains("<")) return;

            fileSetXml = fileSetXml.Substring(fileSetXml.IndexOf("<"));
            fileSetXml = fileSetXml.Replace("FileContents><?xml ", "FileContents>&lt;?xml");
            XmlDocument doc = new XmlDocument();
            //doc.PreserveWhitespace = true;
            try
            {
                //if (!newFileContents.StartsWith("<?xml ")) newFileContents = "<?xml version=\"1.0\"?>" + newFileContents;
                int len = fileSetXml.Length;
                fileSetXml = fileSetXml.Trim((char)(65279));
                int len2 = fileSetXml.Length;
                doc.LoadXml(fileSetXml);
                if (doc.DocumentElement.Name == "FileSet")
                {
                    foreach (XmlElement elem in doc.DocumentElement.SelectNodes("//FileSetFile"))
                    {
                        bool neverOverwrite = true;

                        XmlNode aoNode = elem.SelectSingleNode("AlwaysOverwrite");
                        if (!ReferenceEquals(aoNode, null))
                        {
                            if (aoNode.InnerText == "true") neverOverwrite = false;
                        }
                        else
                        {
                            XmlNode omNode = elem.SelectSingleNode("OverwriteMode");
                            if (!ReferenceEquals(omNode, null) && (omNode.InnerText != "Never"))
                            {
                                neverOverwrite = false;
                            }
                        }

                        XmlNode contentsNode = elem.SelectSingleNode("FileContents");
                        XmlNode binaryContentsNode = elem.SelectSingleNode("BinaryFileContents");
                        XmlNode zippedBinaryContentsNode = elem.SelectSingleNode("ZippedBinaryFileContents");
                        XmlNode zippedTextContentsNode = elem.SelectSingleNode("ZippedTextFileContents");
                        if (ReferenceEquals(zippedTextContentsNode, null))
                        {
                            zippedTextContentsNode = elem.SelectSingleNode("ZippedFileContents");
                        }

                        if (!ReferenceEquals(contentsNode, null))
                        {
                            String contents = contentsNode.InnerXml;
                            foreach (XmlElement fileName in elem.SelectNodes("RelativePath"))
                            {
                                ProcessFileSetFile(String.Format("{0}\\test.xml", basePath), overwriteAll, elem, contents, fileName, basePath);
                            }
                        }
                        else if (!ReferenceEquals(binaryContentsNode, null) ||
                                !ReferenceEquals(zippedBinaryContentsNode, null) ||
                                !ReferenceEquals(zippedTextContentsNode, null))
                        {

                            String contents = String.Empty;
                            if (!ReferenceEquals(binaryContentsNode, null)) contents = binaryContentsNode.InnerXml;
                            else if (!ReferenceEquals(zippedBinaryContentsNode, null)) contents = zippedBinaryContentsNode.InnerXml;
                            else contents = zippedTextContentsNode.InnerXml;

                            foreach (XmlElement fileName in elem.SelectNodes("RelativePath"))
                            {
                                byte[] data = Convert.FromBase64String(contents);

                                var finalName = Path.Combine(basePath, fileName.InnerText.SafeToString().Trim("/".ToCharArray()));
                                var fileInfo = new FileInfo(finalName);

                                if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                                if (!fileInfo.Exists || !neverOverwrite)
                                {

                                    if (!ReferenceEquals(zippedBinaryContentsNode, null))
                                    {
                                        File.WriteAllBytes(fileInfo.FullName, data.Unzip());
                                    }
                                    else if (!ReferenceEquals(zippedTextContentsNode, null))
                                    {

                                        using (StreamWriter sw = new StreamWriter(File.Open(fileInfo.FullName, FileMode.Create), new UTF8Encoding(false)))
                                        {
                                            sw.WriteLine(data.UnzipToString());
                                        }
                                    }
                                    else
                                    {
                                        if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();
                                        var di = new DirectoryInfo(fileInfo.FullName);
                                        if (di.Exists) throw new Exception(String.Format("Invalid filename for result file - {0} is a directory", fileInfo.FullName));
                                        else File.WriteAllBytes(fileInfo.FullName, data);
                                    }

                                }
                            }
                        }
                        else
                        {
                            foreach (XmlElement fileName in elem.SelectNodes("RelativePath"))
                            {
                                var fileInfo = new FileInfo(fileName.InnerText);

                                if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                                if (!fileInfo.Exists || !neverOverwrite)
                                {
                                    WriteAllText(fileName.InnerText, "Can't write binary contents.");
                                }
                            }

                        }
                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                // Ignore failures attempting to write extra files
                throw ex;
            }
        }


        private static void ProcessFileSetFile(String relativePathOfXml, bool overwriteAll, XmlElement elem, String contents, XmlElement fileName, String basePath)
        {
            String relativeFileName = fileName.InnerText;
            relativeFileName = FullFromRelative(relativePathOfXml, relativeFileName.Trim("\\/ \r\n".ToCharArray()));
            //lastOutputFiles.Add(fullFileName);
            String dir = Path.GetDirectoryName(relativeFileName);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            bool fileExists = File.Exists(relativeFileName);
            bool writeFile = true;
            bool neverOverwrite = true;

            XmlNode aoNode = elem.SelectSingleNode("AlwaysOverwrite");
            if (!ReferenceEquals(aoNode, null))
            {
                if (aoNode.InnerText == "true") neverOverwrite = false;
            }
            else
            {
                XmlNode omNode = elem.SelectSingleNode("OverwriteMode");
                if (!ReferenceEquals(omNode, null) && (omNode.InnerText != "Never"))
                {
                    neverOverwrite = false;
                }
            }

            if (fileExists && neverOverwrite)
            {
                writeFile = false;
                //lastOutputFilesSkipped.Add(fullFileName);
            }

            while (contents.Contains("[$$NEWUUID$$]"))
            {
                contents = String.Format("{0}{1}{2}",
                                    contents.Substring(0, contents.IndexOf("[$$NEWUUID$$]")),
                                    Guid.NewGuid(),
                                    contents.Substring(contents.IndexOf("[$$NEWUUID$$]") + "[$$NEWUUID$$]".Length));
            }
            String decodedContent = HttpUtility.HtmlDecode(contents);
            decodedContent = decodedContent.UnwrapCDATA();

            if (overwriteAll || writeFile)
            {
                if (overwriteAll ||
                    !File.Exists(relativeFileName) ||
                    (File.ReadAllText(relativeFileName) != decodedContent))
                {
                    WriteAllText(relativeFileName, decodedContent);
                }
            }
        }

        public static string FullFromRelative(this string rootFullPath, string relativeFileName)
        {
            relativeFileName = relativeFileName.SafeToString().Trim("\r\n\t \\/".ToCharArray());
            FileInfo fi = new FileInfo(Path.Combine(Path.GetDirectoryName(rootFullPath), relativeFileName));
            return fi.FullName;
        }

        public static string RelativeFromFull(this String rootFullPath, String fullPath)
        {
            if (String.IsNullOrEmpty(fullPath)) return fullPath;
            // Trim the path until it doesn't match
            String dir = Path.GetDirectoryName(rootFullPath).Replace("/", "\\");
            String[] dirParts = dir.Split("\\".ToCharArray());
            String[] fullPathParts = fullPath.Replace("/", "\\").Split("\\".ToCharArray());
            int index = 0;
            while (index < fullPathParts.Length && index < dirParts.Length)
            {
                if (String.Equals(fullPathParts[index], dirParts[index], StringComparison.OrdinalIgnoreCase))
                {
                    index++;
                }
                else
                {
                    break;
                }
            }
            String left = String.Join("\\", dirParts.Take(index));
            String[] parts = left.Split("\\/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            String right = String.Join("\\", fullPathParts.Skip(index));
            int count = dirParts.Length - index;
            for (int i = 0; i < count; i++)
            {
                right = "../" + right;
            }
            return right.Replace("/", "\\");
        }


        private static void WriteAllText(string fileName, string fileContents)
        {
            var fi = new FileInfo(fileName);
            if (!fi.Directory.Exists) fi.Directory.Create();
            using (StreamWriter sw = new StreamWriter(File.Open(fileName, FileMode.Create), new UTF8Encoding(false)))
            {
                sw.Write(fileContents.UnwrapCDATA());
            }
            OnFileWritten(fileName);
        }

        public static void SplitFileSetXml(this string fileSetXml, String basePath)
        {
            fileSetXml.SplitFileSetXml(false, basePath);
        }




        public static bool IsLexiconTerm<T>(this StandardPayload<T> payload, LexiconTermEnum termKey)
            where T : StandardPayload<T>, new()
        {
            LexiconTerm term = Lexicon.Terms[termKey];
            return (payload.RoutingKey == term.RoutingKey);
        }
    }
}
