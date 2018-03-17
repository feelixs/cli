using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

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
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequests")]
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
            

        
        
        private static string CreateTranspileRequestStatusWhere(IEnumerable<TranspileRequestStatus> transpileRequestStatuses)
        {
            if (!transpileRequestStatuses.Any()) return "1=1";
            else 
            {
                var idList = transpileRequestStatuses.Select(selectTranspileRequestStatus => String.Format("'{0}'", selectTranspileRequestStatus.TranspileRequestStatusId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspileRequestStatusId in ({0})", csIdList);
            }
        }
        
    }
}