using System;
using System.ComponentModel;
using Newtonsoft.Json;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class AccountHolder
    {
        private void InitPoco()
        {
            
            this.AccountHolderId = Guid.NewGuid();
            
            this.Transpilers = new BindingList<Transpiler>();
            
            this.TranspileRequests = new BindingList<TranspileRequest>();
            
            this.TranspilerHosts = new BindingList<TranspilerHost>();
            
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountHolderId")]
        public Guid AccountHolderId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "EmailAddress")]
        public String EmailAddress { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "HashOfSecret")]
        public String HashOfSecret { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ScreenName")]
        public String ScreenName { get; set; }
    
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Transpilers")] // 
        public BindingList<Transpiler> Transpilers { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequests")] // 
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerHosts")] // 
        public BindingList<TranspilerHost> TranspilerHosts { get; set; }
    }
}