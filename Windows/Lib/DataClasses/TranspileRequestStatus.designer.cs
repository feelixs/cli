using System;
using System.ComponentModel;
using Newtonsoft.Json;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspileRequestStatus
    {
        private void InitPoco()
        {
            
            this.TranspileRequestStatusId = Guid.NewGuid();
            
            this.TranspileRequests = new BindingList<TranspileRequest>();
            
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequestStatusId")]
        public Guid TranspileRequestStatusId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Description")]
        public String Description { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SortOrder")]
        public Int32 SortOrder { get; set; }
    
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequests")] // 
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
    }
}