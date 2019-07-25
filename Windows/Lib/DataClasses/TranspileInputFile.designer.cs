using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspileInputFile
    {
        private void InitPoco()
        {
            
            this.TranspileInputFileId = Guid.NewGuid();
            

        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileInputFileId")]
        public Guid TranspileInputFileId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequestId")]
        public Guid TranspileRequestId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileFileId")]
        public Guid TranspileFileId { get; set; }
    

        

        
        
        private static string CreateTranspileInputFileWhere(IEnumerable<TranspileInputFile> transpileInputFiles, String forignKeyFieldName = "TranspileInputFileId")
        {
            if (!transpileInputFiles.Any()) return "1=1";
            else 
            {
                var idList = transpileInputFiles.Select(selectTranspileInputFile => String.Format("'{0}'", selectTranspileInputFile.TranspileInputFileId));
                var csIdList = String.Join(",", idList);
                return String.Format("{0} in ({1})", forignKeyFieldName, csIdList);
            }
        }
        
    }
}
