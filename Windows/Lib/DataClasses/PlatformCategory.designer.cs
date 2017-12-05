using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class PlatformCategory
    {
        private void InitPoco()
        {
            
            this.PlatformCategoryId = Guid.NewGuid();
            
                this.TranspilerPlatforms = new BindingList<TranspilerPlatform>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "PlatformCategoryId")]
        public Guid PlatformCategoryId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SortOrder")]
        public Int32 SortOrder { get; set; }
    
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
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerPlatforms")]
        public BindingList<TranspilerPlatform> TranspilerPlatforms { get; set; }
            
        /// <summary>
        /// Find the related TranspilerPlatforms (from the list provided) and attach them locally to the TranspilerPlatforms list.
        /// </summary>
        public void LoadTranspilerPlatforms(IEnumerable<TranspilerPlatform> transpilerPlatforms)
        {
            transpilerPlatforms.Where(whereTranspilerPlatform => whereTranspilerPlatform.PlatformCategoryId == this.PlatformCategoryId)
                    .ToList()
                    .ForEach(feTranspilerPlatform => this.TranspilerPlatforms.Add(feTranspilerPlatform));
        }
        

        

        private static string CreatePlatformCategoryWhere(IEnumerable<PlatformCategory> platformCategories)
        {
            if (!platformCategories.Any()) return "1=1";
            else 
            {
                var idList = platformCategories.Select(selectPlatformCategory => String.Format("'{0}'", selectPlatformCategory.PlatformCategoryId));
                var csIdList = String.Join(",", idList);
                return String.Format("PlatformCategoryId in ({0})", csIdList);
            }
        }
        
    }
}