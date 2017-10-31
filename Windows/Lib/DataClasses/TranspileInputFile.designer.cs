using System;
using System.ComponentModel;
using Newtonsoft.Json;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspileInputFile
    {
        private void InitPoco()
        {
            
            this.TranspileInputFileId = Guid.NewGuid();
            
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileInputFileId")]
        public Guid TranspileInputFileId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequestId")]
        public Guid TranspileRequestId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileFileId")]
        public Guid TranspileFileId { get; set; }
    
        
    }
}