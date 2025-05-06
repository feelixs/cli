using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace AIC.Lib.DataClasses
{                            
    public partial class SharedDirectory
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SharedDirectoryId")]
        public String SharedDirectoryId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectory")]
        [RemoteIsCollection]
        public String AICDirectory { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Frendiship")]
        [RemoteIsCollection]
        public String Frendiship { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FrendishipRequestedFriend")]
        [RemoteIsCollection]
        public String FrendishipRequestedFriend { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectoryParentOwner")]
        public String AICDirectoryParentOwner { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectoryParentAICaptureUserId")]
        public String AICDirectoryParentAICaptureUserId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AllowedUser")]
        [RemoteIsCollection]
        public String AllowedUser { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AllowedAICaptureUserId")]
        [RemoteIsCollection]
        public String AllowedAICaptureUserId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectoryParentProjectOwner")]
        [RemoteIsCollection]
        public String AICDirectoryParentProjectOwner { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectoryParentProjectOwnerAICaptureUserId")]
        [RemoteIsCollection]
        public String AICDirectoryParentProjectOwnerAICaptureUserId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectoryParentWorkspaceOwner")]
        [RemoteIsCollection]
        public String AICDirectoryParentWorkspaceOwner { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICDirectoryParentWorkspaceOwnerAICaptureUserId")]
        [RemoteIsCollection]
        public String AICDirectoryParentWorkspaceOwnerAICaptureUserId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsOrphaned")]
        public Nullable<Int32> IsOrphaned { get; set; }
    

        

        
        
        
    }
}
