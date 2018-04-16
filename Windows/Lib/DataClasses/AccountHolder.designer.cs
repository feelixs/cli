using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

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
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountSubscriptionFee")]
        public Nullable<decimal> AccountSubscriptionFee { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Description")]
        public String Description { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountLocked")]
        public Boolean AccountLocked { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Transpilers")]
        public BindingList<Transpiler> Transpilers { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequests")]
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerHosts")]
        public BindingList<TranspilerHost> TranspilerHosts { get; set; }
            

        
        
        private static string CreateAccountHolderWhere(IEnumerable<AccountHolder> accountHolders, String forignKeyFieldName = "AccountHolderId")
        {
            if (!accountHolders.Any()) return "1=1";
            else 
            {
                var idList = accountHolders.Select(selectAccountHolder => String.Format("'{0}'", selectAccountHolder.AccountHolderId));
                var csIdList = String.Join(",", idList);
                return String.Format("{0} in ({1})", forignKeyFieldName, csIdList);
            }
        }
        
    }
}
