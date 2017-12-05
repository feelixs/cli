using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class Transpiler
    {
        private void InitPoco()
        {
            
            this.TranspilerId = Guid.NewGuid();
            
                this.TranspileRequests = new BindingList<TranspileRequest>();
            
                this.TranspilerInstances = new BindingList<TranspilerInstance>();
            
                this.TranspilerVersions = new BindingList<TranspilerVersion>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerId")]
        public Guid TranspilerId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountHolderId")]
        public Guid AccountHolderId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerPlatformId")]
        public Nullable<Guid> TranspilerPlatformId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "DisplayName")]
        public String DisplayName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Description")]
        public String Description { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public Nullable<DateTime> CreatedOn { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsActive")]
        public Boolean IsActive { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CurrentRoutingKey")]
        public String CurrentRoutingKey { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsPrivate")]
        public Boolean IsPrivate { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LowerName")]
        public String LowerName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "UpperName")]
        public String UpperName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LowerHyphenName")]
        public String LowerHyphenName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ReadMeMD")]
        public String ReadMeMD { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "InputDescriptionMD")]
        public String InputDescriptionMD { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OutputDescriptionMD")]
        public String OutputDescriptionMD { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ExampleMD")]
        public String ExampleMD { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "InputFileTypeId")]
        public Nullable<Guid> InputFileTypeId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OutputFileTypeId")]
        public Nullable<Guid> OutputFileTypeId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "UsageCount")]
        public Int32 UsageCount { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Category")]
        public String Category { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsRecommended")]
        public Boolean IsRecommended { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequests")]
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
            
        /// <summary>
        /// Find the related TranspileRequests (from the list provided) and attach them locally to the TranspileRequests list.
        /// </summary>
        public void LoadTranspileRequests(IEnumerable<TranspileRequest> transpileRequests)
        {
            transpileRequests.Where(whereTranspileRequest => whereTranspileRequest.TranspilerId == this.TranspilerId)
                    .ToList()
                    .ForEach(feTranspileRequest => this.TranspileRequests.Add(feTranspileRequest));
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerInstances")]
        public BindingList<TranspilerInstance> TranspilerInstances { get; set; }
            
        /// <summary>
        /// Find the related TranspilerInstances (from the list provided) and attach them locally to the TranspilerInstances list.
        /// </summary>
        public void LoadTranspilerInstances(IEnumerable<TranspilerInstance> transpilerInstances)
        {
            transpilerInstances.Where(whereTranspilerInstance => whereTranspilerInstance.TranspilerId == this.TranspilerId)
                    .ToList()
                    .ForEach(feTranspilerInstance => this.TranspilerInstances.Add(feTranspilerInstance));
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerVersions")]
        public BindingList<TranspilerVersion> TranspilerVersions { get; set; }
            
        /// <summary>
        /// Find the related TranspilerVersions (from the list provided) and attach them locally to the TranspilerVersions list.
        /// </summary>
        public void LoadTranspilerVersions(IEnumerable<TranspilerVersion> transpilerVersions)
        {
            transpilerVersions.Where(whereTranspilerVersion => whereTranspilerVersion.TranspilerId == this.TranspilerId)
                    .ToList()
                    .ForEach(feTranspilerVersion => this.TranspilerVersions.Add(feTranspilerVersion));
        }
        

        

        private static string CreateTranspilerWhere(IEnumerable<Transpiler> transpilers)
        {
            if (!transpilers.Any()) return "1=1";
            else 
            {
                var idList = transpilers.Select(selectTranspiler => String.Format("'{0}'", selectTranspiler.TranspilerId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspilerId in ({0})", csIdList);
            }
        }
        
    }
}