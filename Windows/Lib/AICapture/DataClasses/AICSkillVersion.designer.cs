using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace AIC.Lib.DataClasses
{                            
    public partial class AICSkillVersion
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICSkillVersionId")]
        public String AICSkillVersionId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICSkill")]
        [RemoteIsCollection]
        public String AICSkill { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICSkillAssociatedMethod")]
        [RemoteIsCollection]
        public String AICSkillAssociatedMethod { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICSkillName")]
        [RemoteIsCollection]
        public String AICSkillName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICSkillNotes")]
        [RemoteIsCollection]
        public String AICSkillNotes { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Version")]
        public Nullable<decimal> Version { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Notes")]
        public String Notes { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ImplementationAICSkillSteps")]
        [RemoteIsCollection]
        public String[] ImplementationAICSkillSteps { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ReferencedByAICSkillSteps")]
        [RemoteIsCollection]
        public String[] ReferencedByAICSkillSteps { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SkillIsPublic")]
        [RemoteIsCollection]
        public Nullable<Boolean> SkillIsPublic { get; set; }
    

        

        
        
        
    }
}
