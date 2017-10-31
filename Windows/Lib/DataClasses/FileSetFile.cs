using System;
using System.Collections.Generic;

namespace SSoTme.OST.Lib.DataClasses
{
    public partial class FileSetFile
    {
        public string OriginalRelativePath { get; set; }

        public override string ToString()
        {
            return this.RelativePath;
        }

        internal void ClearContents()
        {
            this.BinaryFileContents = null;
            this.FileContents = null;
            this.ZippedBinaryFileContents = null;
            this.ZippedFileContents = null;
            this.ZippedTextFileContents = null;
        }
    }


}