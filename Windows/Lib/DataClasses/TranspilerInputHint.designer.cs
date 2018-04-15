using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerInputHint
    {
        private void InitPoco()
        {
            
            this.TranspilerInputHintId = Guid.NewGuid();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerInputHintId")]
        public Guid TranspilerInputHintId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerVersionId")]
        public Guid TranspilerVersionId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerFileTypeId")]
        public Nullable<Guid> TranspilerFileTypeId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "NameContains")]
        public String NameContains { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SortOrder")]
        public Int32 SortOrder { get; set; }
    

        

        
        
        private static string CreateTranspilerInputHintWhere(IEnumerable<TranspilerInputHint> transpilerInputHints, String forignKeyFieldName = "TranspilerInputHintId")
        {
            if (!transpilerInputHints.Any()) return "1=1";
            else 
            {
                var idList = transpilerInputHints.Select(selectTranspilerInputHint => String.Format("'{0}'", selectTranspilerInputHint.TranspilerInputHintId));
                var csIdList = String.Join(",", idList);
                return String.Format("{0} in ({1})", forignKeyFieldName, csIdList);
            }
        }
        
    }
}
