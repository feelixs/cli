using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class FileSet
    {
        private void InitPoco()
        {
            
            this.FileSetId = Guid.NewGuid();
            
                this.FileSetFiles = new BindingList<FileSetFile>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FileSetId")]
        public Guid FileSetId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public Nullable<DateTime> CreatedOn { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FileSetFiles")]
        public BindingList<FileSetFile> FileSetFiles { get; set; }
            

        
        
        private static string CreateFileSetWhere(IEnumerable<FileSet> fileSets, String forignKeyFieldName = "FileSetId")
        {
            if (!fileSets.Any()) return "1=1";
            else 
            {
                var idList = fileSets.Select(selectFileSet => String.Format("'{0}'", selectFileSet.FileSetId));
                var csIdList = String.Join(",", idList);
                return String.Format("{0} in ({1})", forignKeyFieldName, csIdList);
            }
        }
        
    }
}
