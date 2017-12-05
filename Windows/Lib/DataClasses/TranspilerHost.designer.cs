using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerHost
    {
        private void InitPoco()
        {
            
            this.TranspilerHostId = Guid.NewGuid();
            
                this.TranspilerInstances = new BindingList<TranspilerInstance>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerHostId")]
        public Guid TranspilerHostId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountHolderId")]
        public Guid AccountHolderId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "BaseRoutingKey")]
        public String BaseRoutingKey { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerHostIndex")]
        public Int32 TranspilerHostIndex { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LastPing")]
        public Nullable<DateTime> LastPing { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TimeoutSeconds")]
        public Int32 TimeoutSeconds { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsTerminated")]
        public Boolean IsTerminated { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TermatedOn")]
        public Nullable<DateTime> TermatedOn { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerInstances")]
        public BindingList<TranspilerInstance> TranspilerInstances { get; set; }
            
        /// <summary>
        /// Find the related TranspilerInstances (from the list provided) and attach them locally to the TranspilerInstances list.
        /// </summary>
        public void LoadTranspilerInstances(IEnumerable<TranspilerInstance> transpilerInstances)
        {
            transpilerInstances.Where(whereTranspilerInstance => whereTranspilerInstance.TranspilerHostId == this.TranspilerHostId)
                    .ToList()
                    .ForEach(feTranspilerInstance => this.TranspilerInstances.Add(feTranspilerInstance));
        }
        

        

        private static string CreateTranspilerHostWhere(IEnumerable<TranspilerHost> transpilerHosts)
        {
            if (!transpilerHosts.Any()) return "1=1";
            else 
            {
                var idList = transpilerHosts.Select(selectTranspilerHost => String.Format("'{0}'", selectTranspilerHost.TranspilerHostId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspilerHostId in ({0})", csIdList);
            }
        }
        
    }
}