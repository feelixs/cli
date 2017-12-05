using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class FileSetFile
    {
        private void InitPoco()
        {
            
            this.FileSetFileId = Guid.NewGuid();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FileSetFileId")]
        public Guid FileSetFileId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FileSetId")]
        public Guid FileSetId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RelativePath")]
        public String RelativePath { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FileContents")]
        public String FileContents { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ZippedFileContents")]
        public Byte[] ZippedFileContents { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "BinaryFileContents")]
        public Byte[] BinaryFileContents { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ZippedTextFileContents")]
        public Byte[] ZippedTextFileContents { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ZippedBinaryFileContents")]
        public Byte[] ZippedBinaryFileContents { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AlwaysOverwrite")]
        public Boolean AlwaysOverwrite { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SkipClean")]
        public Boolean SkipClean { get; set; }
    

        

        

        private static string CreateFileSetFileWhere(IEnumerable<FileSetFile> fileSetFiles)
        {
            if (!fileSetFiles.Any()) return "1=1";
            else 
            {
                var idList = fileSetFiles.Select(selectFileSetFile => String.Format("'{0}'", selectFileSetFile.FileSetFileId));
                var csIdList = String.Join(",", idList);
                return String.Format("FileSetFileId in ({0})", csIdList);
            }
        }
        
    }
}