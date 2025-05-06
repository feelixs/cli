using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace AIC.Lib.DataClasses
{                            
    public partial class AICContextOutput
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextOutputId")]
        public String AICContextOutputId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Notes")]
        public String Notes { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Language")]
        [RemoteIsCollection]
        public String Language { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LanguageName")]
        [RemoteIsCollection]
        public String LanguageName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LanguageCode")]
        [RemoteIsCollection]
        public String LanguageCode { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContext")]
        [RemoteIsCollection]
        public String AICContext { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextAICProject")]
        [RemoteIsCollection]
        public String AICContextAICProject { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextProjectCode")]
        [RemoteIsCollection]
        public String AICContextProjectCode { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextContextType")]
        [RemoteIsCollection]
        public String AICContextContextType { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextContextName")]
        [RemoteIsCollection]
        public String AICContextContextName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextParentContextName")]
        [RemoteIsCollection]
        public String AICContextParentContextName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextFullContextName")]
        [RemoteIsCollection]
        public String AICContextFullContextName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextRelativePath")]
        [RemoteIsCollection]
        public String AICContextRelativePath { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextAICContextCategory")]
        [RemoteIsCollection]
        public String AICContextAICContextCategory { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextCategoryName")]
        [RemoteIsCollection]
        public String AICContextCategoryName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextNotes")]
        [RemoteIsCollection]
        public String AICContextNotes { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextAICContextType")]
        [RemoteIsCollection]
        public String AICContextAICContextType { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextFileContents")]
        [RemoteIsCollection]
        public String AICContextFileContents { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextSingleSourceOfTruth")]
        [RemoteIsCollection]
        public String AICContextSingleSourceOfTruth { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextM2MTransformer")]
        [RemoteIsCollection]
        public String AICContextM2MTransformer { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextSSoTMarkdown")]
        [RemoteIsCollection]
        public String AICContextSSoTMarkdown { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextSSoTSchema")]
        [RemoteIsCollection]
        public String AICContextSSoTSchema { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextParentContext")]
        [RemoteIsCollection]
        public String AICContextParentContext { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextOutputType")]
        [RemoteIsCollection]
        public String AICContextOutputType { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICContextOutputTypeDisplayName")]
        [RemoteIsCollection]
        public String AICContextOutputTypeDisplayName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Temp")]
        public String[] Temp { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ContextCategory")]
        public String ContextCategory { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CategoryName")]
        public String CategoryName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OutputType")]
        public String OutputType { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SingleSourceOfTruth")]
        public String SingleSourceOfTruth { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FileContents")]
        public String FileContents { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "M2MTransformer")]
        public String M2MTransformer { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SSoTMarkdown")]
        public String SSoTMarkdown { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SSoTSchema")]
        public String SSoTSchema { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ContextRawOutput")]
        public String ContextRawOutput { get; set; }
    

        

        
        
        
    }
}
