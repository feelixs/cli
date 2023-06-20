using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace AIC.Lib.DataClasses
{                            
    public partial class ContextSkill
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ContextSkillId")]
        public String ContextSkillId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Notes")]
        public String Notes { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContext")]
        [RemoteIsCollection]
        public String AICContext { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ContextSkillAICSlashCommand")]
        [RemoteIsCollection]
        public String ContextSkillAICSlashCommand { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ContextSkillAssociatedMethod")]
        [RemoteIsCollection]
        public String ContextSkillAssociatedMethod { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ContextSkillAICSkillName")]
        [RemoteIsCollection]
        public String ContextSkillAICSkillName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ContextSkillAICSkillVersion")]
        [RemoteIsCollection]
        public String ContextSkillAICSkillVersion { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ContextSkillIsPublic")]
        [RemoteIsCollection]
        public Nullable<Boolean> ContextSkillIsPublic { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ContextSkillCleanName")]
        [RemoteIsCollection]
        public String ContextSkillCleanName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextName")]
        [RemoteIsCollection]
        public String AICContextName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextAICProject")]
        [RemoteIsCollection]
        public String AICContextAICProject { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextProjectCode")]
        [RemoteIsCollection]
        public String AICContextProjectCode { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextContextName")]
        [RemoteIsCollection]
        public String AICContextContextName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextParentContextName")]
        [RemoteIsCollection]
        public String AICContextParentContextName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextFullContextName")]
        [RemoteIsCollection]
        public String AICContextFullContextName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ContextSkillPriority")]
        [RemoteIsCollection]
        public Nullable<Int32> ContextSkillPriority { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICSkill")]
        [RemoteIsCollection]
        public String AICSkill { get; set; }
    

        

        
        
        
    }
}
