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
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerInstances")]
        public BindingList<TranspilerInstance> TranspilerInstances { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ReplacedByTranspilerVersionId_TranspilerVersions")]
        public BindingList<TranspilerVersion> ReplacedByTranspilerVersionId_TranspilerVersions { get; set; }
            

        
        
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