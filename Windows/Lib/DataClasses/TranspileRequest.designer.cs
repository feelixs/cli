using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspileRequest
    {
        private void InitPoco()
        {
            
            this.TranspileRequestId = Guid.NewGuid();
            
                this.LastTranspilerRequestId_ProjectTranspilers = new BindingList<ProjectTranspiler>();
            
                this.TranspileInputFiles = new BindingList<TranspileInputFile>();
            
                this.TranspileOutputFiles = new BindingList<TranspileOutputFile>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequestId")]
        public Guid TranspileRequestId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequestStatusId")]
        public Guid TranspileRequestStatusId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerId")]
        public Guid TranspilerId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountHolderId")]
        public Guid AccountHolderId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ZippedInputFileSet")]
        public Byte[] ZippedInputFileSet { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ZippedOutputFileSet")]
        public Byte[] ZippedOutputFileSet { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerInstanceId")]
        public Nullable<Guid> TranspilerInstanceId { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LastTranspilerRequestId_ProjectTranspilers")]
        public BindingList<ProjectTranspiler> LastTranspilerRequestId_ProjectTranspilers { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileInputFiles")]
        public BindingList<TranspileInputFile> TranspileInputFiles { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileOutputFiles")]
        public BindingList<TranspileOutputFile> TranspileOutputFiles { get; set; }
            

        

        private static string CreateTranspileRequestWhere(IEnumerable<TranspileRequest> transpileRequests)
        {
            if (!transpileRequests.Any()) return "1=1";
            else 
            {
                var idList = transpileRequests.Select(selectTranspileRequest => String.Format("'{0}'", selectTranspileRequest.TranspileRequestId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspileRequestId in ({0})", csIdList);
            }
        }
        
    }
}