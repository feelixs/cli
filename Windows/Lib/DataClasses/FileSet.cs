using SSoTme.OST.Lib.Extensions;
using System;
using System.ComponentModel;
using System.IO;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class FileSet 
    {
        public FileSet()
        {
            this.InitPoco();
        }

        public void AddResourceFolder(string v)
        {
            throw new NotImplementedException();
        }

        internal void WriteTo(DirectoryInfo rootDirInfo)
        {
            this.ToXml().SplitFileSetXml(false, rootDirInfo.FullName);
        }
        // Your code goes here...
        // The "default" code is in the designer file
    }
}