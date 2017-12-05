using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerInstance
    {
        private void InitPoco()
        {
            
            this.TranspilerInstanceId = Guid.NewGuid();
            
                this.TranspileRequests = new BindingList<TranspileRequest>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerInstanceId")]
        public Guid TranspilerInstanceId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerHostId")]
        public Guid TranspilerHostId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerId")]
        public Nullable<Guid> TranspilerId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerVersionId")]
        public Nullable<Guid> TranspilerVersionId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RoutingKey")]
        public String RoutingKey { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsTerminated")]
        public Boolean IsTerminated { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequests")]
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
            
        /// <summary>
        /// Find the related TranspileRequests (from the list provided) and attach them locally to the TranspileRequests list.
        /// </summary>
        public void LoadTranspileRequests(IEnumerable<TranspileRequest> transpileRequests)
        {
            transpileRequests.Where(whereTranspileRequest => whereTranspileRequest.TranspilerInstanceId == this.TranspilerInstanceId)
                    .ToList()
                    .ForEach(feTranspileRequest => this.TranspileRequests.Add(feTranspileRequest));
        }
        

        

        private static string CreateTranspilerInstanceWhere(IEnumerable<TranspilerInstance> transpilerInstances)
        {
            if (!transpilerInstances.Any()) return "1=1";
            else 
            {
                var idList = transpilerInstances.Select(selectTranspilerInstance => String.Format("'{0}'", selectTranspilerInstance.TranspilerInstanceId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspilerInstanceId in ({0})", csIdList);
            }
        }
        
    }
}