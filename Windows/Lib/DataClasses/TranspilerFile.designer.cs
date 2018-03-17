using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerFile
    {
        private void InitPoco()
        {
            
            this.TranspilerFileId = Guid.NewGuid();
            
                this.TranspileFileId_TranspileInputFiles = new BindingList<TranspileInputFile>();
            
                this.TranspileFileId_TranspileOutputFiles = new BindingList<TranspileOutputFile>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerFileId")]
        public Guid TranspilerFileId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerFileTypeId")]
        public Guid TranspilerFileTypeId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ZippedBytes")]
        public Byte[] ZippedBytes { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileFileId_TranspileInputFiles")]
        public BindingList<TranspileInputFile> TranspileFileId_TranspileInputFiles { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileFileId_TranspileOutputFiles")]
        public BindingList<TranspileOutputFile> TranspileFileId_TranspileOutputFiles { get; set; }
            

        
        
        private static string CreateTranspilerFileWhere(IEnumerable<TranspilerFile> transpilerFiles)
        {
            if (!transpilerFiles.Any()) return "1=1";
            else 
            {
                var idList = transpilerFiles.Select(selectTranspilerFile => String.Format("'{0}'", selectTranspilerFile.TranspilerFileId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspilerFileId in ({0})", csIdList);
            }
        }
        
    }
}