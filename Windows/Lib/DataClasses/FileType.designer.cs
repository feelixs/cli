using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class FileType
    {
        private void InitPoco()
        {
            
            this.FileTypeId = Guid.NewGuid();
            
                this.InputFileTypeId_Transpilers = new BindingList<Transpiler>();
            
                this.OutputFileTypeId_Transpilers = new BindingList<Transpiler>();
            

        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FileTypeId")]
        public Guid FileTypeId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Description")]
        public String Description { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "MimeType")]
        public String MimeType { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsTemplate")]
        public Boolean IsTemplate { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsFileType")]
        public Boolean IsFileType { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "PNGImage")]
        public String PNGImage { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "UsageCount")]
        public Int32 UsageCount { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "InputFileTypeId_Transpilers")]
        public BindingList<Transpiler> InputFileTypeId_Transpilers { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OutputFileTypeId_Transpilers")]
        public BindingList<Transpiler> OutputFileTypeId_Transpilers { get; set; }
            

        
        
        private static string CreateFileTypeWhere(IEnumerable<FileType> fileTypes, String forignKeyFieldName = "FileTypeId")
        {
            if (!fileTypes.Any()) return "1=1";
            else 
            {
                var idList = fileTypes.Select(selectFileType => String.Format("'{0}'", selectFileType.FileTypeId));
                var csIdList = String.Join(",", idList);
                return String.Format("{0} in ({1})", forignKeyFieldName, csIdList);
            }
        }
        
    }
}
