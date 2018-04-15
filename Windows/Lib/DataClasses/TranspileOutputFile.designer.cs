using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspileOutputFile
    {
        private void InitPoco()
        {
            
            this.TranspileOutputFileId = Guid.NewGuid();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileOutputFileId")]
        public Guid TranspileOutputFileId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequestId")]
        public Guid TranspileRequestId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileFileId")]
        public Guid TranspileFileId { get; set; }
    

        

        
        
        private static string CreateTranspileOutputFileWhere(IEnumerable<TranspileOutputFile> transpileOutputFiles, String forignKeyFieldName = "TranspileOutputFileId")
        {
            if (!transpileOutputFiles.Any()) return "1=1";
            else 
            {
                var idList = transpileOutputFiles.Select(selectTranspileOutputFile => String.Format("'{0}'", selectTranspileOutputFile.TranspileOutputFileId));
                var csIdList = String.Join(",", idList);
                return String.Format("{0} in ({1})", forignKeyFieldName, csIdList);
            }
        }
        
    }
}
