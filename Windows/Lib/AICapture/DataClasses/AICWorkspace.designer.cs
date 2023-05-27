using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace AIC.Lib.DataClasses
{                            
    public partial class AICWorkspace
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICWorkspaceId")]
        public String AICWorkspaceId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Owner")]
        [RemoteIsCollection]
        public String Owner { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OwnerAICaptureUserId")]
        [RemoteIsCollection]
        public String OwnerAICaptureUserId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OwnerEmailAddress")]
        [RemoteIsCollection]
        public String OwnerEmailAddress { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OwnerWorkspacesPath")]
        [RemoteIsCollection]
        public String OwnerWorkspacesPath { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectory")]
        [RemoteIsCollection]
        public String AICDirectory { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SharedWithAllowedUsersfromAICDirectories")]
        [RemoteIsCollection]
        public String SharedWithAllowedUsersfromAICDirectories { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SharedWithAllowedUsersfromAICDirectoriescopy")]
        [RemoteIsCollection]
        public String SharedWithAllowedUsersfromAICDirectoriescopy { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectorySharedWith")]
        [RemoteIsCollection]
        public String AICDirectorySharedWith { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "DefaultRelativePath")]
        public String DefaultRelativePath { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICProjects")]
        [RemoteIsCollection]
        public String[] AICProjects { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectorySharedWithUserIds")]
        [RemoteIsCollection]
        public String AICDirectorySharedWithUserIds { get; set; }
    

        

        
        
        
    }
}
