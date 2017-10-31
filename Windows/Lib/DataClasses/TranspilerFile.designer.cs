using System;
using System.ComponentModel;
using Newtonsoft.Json;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerFile
    {
        private void InitPoco()
        {
            
            this.TranspilerFileId = Guid.NewGuid();
            
            this.TranspileInputFiles = new BindingList<TranspileInputFile>();
            
            this.TranspileOutputFiles = new BindingList<TranspileOutputFile>();
            
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerFileId")]
        public Guid TranspilerFileId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerFileTypeId")]
        public Guid TranspilerFileTypeId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ZippedBytes")]
        public Byte[] ZippedBytes { get; set; }
    
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileInputFiles")] // 
        public BindingList<TranspileInputFile> TranspileInputFiles { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileOutputFiles")] // 
        public BindingList<TranspileOutputFile> TranspileOutputFiles { get; set; }
    }
}