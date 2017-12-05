using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerVersion
    {
        private void InitPoco()
        {
            
            this.TranspilerVersionId = Guid.NewGuid();
            
                this.TranspilerFileTypeId_TranspilerInputHints = new BindingList<TranspilerInputHint>();
            
                this.TranspilerInstances = new BindingList<TranspilerInstance>();
            
                this.ReplacedByTranspilerVersionId_TranspilerVersions = new BindingList<TranspilerVersion>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerVersionId")]
        public Guid TranspilerVersionId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerId")]
        public Guid TranspilerId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Description")]
        public String Description { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public Nullable<DateTime> CreatedOn { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsActive")]
        public Boolean IsActive { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ReplacedByTranspilerVersionId")]
        public Nullable<Guid> ReplacedByTranspilerVersionId { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerFileTypeId_TranspilerInputHints")]
        public BindingList<TranspilerInputHint> TranspilerFileTypeId_TranspilerInputHints { get; set; }
            
        /// <summary>
        /// Find the related TranspilerInputHints (from the list provided) and attach them locally to the TranspilerInputHints list.
        /// </summary>
        public void LoadTranspilerInputHints(IEnumerable<TranspilerInputHint> transpilerInputHints)
        {
            transpilerInputHints.Where(whereTranspilerInputHint => whereTranspilerInputHint.TranspilerFileTypeId == this.TranspilerVersionId)
                    .ToList()
                    .ForEach(feTranspilerInputHint => this.TranspilerFileTypeId_TranspilerInputHints.Add(feTranspilerInputHint));
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerInstances")]
        public BindingList<TranspilerInstance> TranspilerInstances { get; set; }
            
        /// <summary>
        /// Find the related TranspilerInstances (from the list provided) and attach them locally to the TranspilerInstances list.
        /// </summary>
        public void LoadTranspilerInstances(IEnumerable<TranspilerInstance> transpilerInstances)
        {
            transpilerInstances.Where(whereTranspilerInstance => whereTranspilerInstance.TranspilerVersionId == this.TranspilerVersionId)
                    .ToList()
                    .ForEach(feTranspilerInstance => this.TranspilerInstances.Add(feTranspilerInstance));
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ReplacedByTranspilerVersionId_TranspilerVersions")]
        public BindingList<TranspilerVersion> ReplacedByTranspilerVersionId_TranspilerVersions { get; set; }
            
        /// <summary>
        /// Find the related TranspilerVersions (from the list provided) and attach them locally to the TranspilerVersions list.
        /// </summary>
        public void LoadTranspilerVersions(IEnumerable<TranspilerVersion> transpilerVersions)
        {
            transpilerVersions.Where(whereTranspilerVersion => whereTranspilerVersion.ReplacedByTranspilerVersionId == this.TranspilerVersionId)
                    .ToList()
                    .ForEach(feTranspilerVersion => this.ReplacedByTranspilerVersionId_TranspilerVersions.Add(feTranspilerVersion));
        }
        

        

        private static string CreateTranspilerVersionWhere(IEnumerable<TranspilerVersion> transpilerVersions)
        {
            if (!transpilerVersions.Any()) return "1=1";
            else 
            {
                var idList = transpilerVersions.Select(selectTranspilerVersion => String.Format("'{0}'", selectTranspilerVersion.TranspilerVersionId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspilerVersionId in ({0})", csIdList);
            }
        }
        
    }
}