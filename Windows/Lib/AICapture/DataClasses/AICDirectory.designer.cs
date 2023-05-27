using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace AIC.Lib.DataClasses
{                            
    public partial class AICDirectory
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectoryId")]
        public String AICDirectoryId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Notes")]
        public String Notes { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentProject")]
        [RemoteIsCollection]
        public String ParentProject { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentProjectOwner")]
        [RemoteIsCollection]
        public String ParentProjectOwner { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentProjectOwnerEmailAddress")]
        [RemoteIsCollection]
        public String ParentProjectOwnerEmailAddress { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentProjectOwnerAICaptureUserId")]
        [RemoteIsCollection]
        public String ParentProjectOwnerAICaptureUserId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentProjectOwnerProjectsPath")]
        [RemoteIsCollection]
        public String ParentProjectOwnerProjectsPath { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentProjectDefaultProjectPath")]
        [RemoteIsCollection]
        public String ParentProjectDefaultProjectPath { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentWorkspace")]
        [RemoteIsCollection]
        public String ParentWorkspace { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentWorkspaceOwner")]
        [RemoteIsCollection]
        public String ParentWorkspaceOwner { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentWorkspaceOwnerWorkspacesPath")]
        [RemoteIsCollection]
        public String ParentWorkspaceOwnerWorkspacesPath { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentWorkspaceOwnerAICaptureUserId")]
        [RemoteIsCollection]
        public String ParentWorkspaceOwnerAICaptureUserId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentWorkspaceDefaultRelativePath")]
        [RemoteIsCollection]
        public String ParentWorkspaceDefaultRelativePath { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RelativePath")]
        public String RelativePath { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SharedWith")]
        [RemoteIsCollection]
        public String[] SharedWith { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SharedWithAllowedUsers")]
        [RemoteIsCollection]
        public String[] SharedWithAllowedUsers { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SharedWithAllowedAICaptureUserIds")]
        [RemoteIsCollection]
        public String[] SharedWithAllowedAICaptureUserIds { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICFiles")]
        [RemoteIsCollection]
        public String[] AICFiles { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Test")]
        public Nullable<Int32> Test { get; set; }
    

        

        
        
        
    }
}
