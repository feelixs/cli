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
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Transpilers")]
        public BindingList<Transpiler> Transpilers { get; set; }
            
        /// <summary>
        /// Find the related Transpilers (from the list provided) and attach them locally to the Transpilers list.
        /// </summary>
        public void LoadTranspilers(IEnumerable<Transpiler> transpilers)
        {
            transpilers.Where(whereTranspiler => whereTranspiler.AccountHolderId == this.AccountHolderId)
                    .ToList()
                    .ForEach(feTranspiler => this.Transpilers.Add(feTranspiler));
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequests")]
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
            
        /// <summary>
        /// Find the related TranspileRequests (from the list provided) and attach them locally to the TranspileRequests list.
        /// </summary>
        public void LoadTranspileRequests(IEnumerable<TranspileRequest> transpileRequests)
        {
            transpileRequests.Where(whereTranspileRequest => whereTranspileRequest.AccountHolderId == this.AccountHolderId)
                    .ToList()
                    .ForEach(feTranspileRequest => this.TranspileRequests.Add(feTranspileRequest));
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerHosts")]
        public BindingList<TranspilerHost> TranspilerHosts { get; set; }
            
        /// <summary>
        /// Find the related TranspilerHosts (from the list provided) and attach them locally to the TranspilerHosts list.
        /// </summary>
        public void LoadTranspilerHosts(IEnumerable<TranspilerHost> transpilerHosts)
        {
            transpilerHosts.Where(whereTranspilerHost => whereTranspilerHost.AccountHolderId == this.AccountHolderId)
                    .ToList()
                    .ForEach(feTranspilerHost => this.TranspilerHosts.Add(feTranspilerHost));
        }
        

        

        private static string CreateAccountHolderWhere(IEnumerable<AccountHolder> accountHolders)
        {
            if (!accountHolders.Any()) return "1=1";
            else 
            {
                var idList = accountHolders.Select(selectAccountHolder => String.Format("'{0}'", selectAccountHolder.AccountHolderId));
                var csIdList = String.Join(",", idList);
                return String.Format("AccountHolderId in ({0})", csIdList);
            }
        }
        
    }
}