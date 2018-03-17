using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerPlatform
    {
        private void InitPoco()
        {
            
            this.TranspilerPlatformId = Guid.NewGuid();
            
                this.Transpilers = new BindingList<Transpiler>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerPlatformId")]
        public Guid TranspilerPlatformId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "PlatformCategoryId")]
        public Guid PlatformCategoryId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "DescriptionMD")]
        public String DescriptionMD { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "DisplayName")]
        public String DisplayName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public Nullable<DateTime> CreatedOn { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsActive")]
        public Boolean IsActive { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LowerName")]
        public String LowerName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "UpperName")]
        public String UpperName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LowerHyphenName")]
        public String LowerHyphenName { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Transpilers")]
        public BindingList<Transpiler> Transpilers { get; set; }
            

        
        
        private static string CreateTranspilerPlatformWhere(IEnumerable<TranspilerPlatform> transpilerPlatforms)
        {
            if (!transpilerPlatforms.Any()) return "1=1";
            else 
            {
                var idList = transpilerPlatforms.Select(selectTranspilerPlatform => String.Format("'{0}'", selectTranspilerPlatform.TranspilerPlatformId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspilerPlatformId in ({0})", csIdList);
            }
        }
        
    }
}