using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace AIC.Lib.DataClasses
{                            
    public partial class AppUser
    {
        private void InitPoco()
        {
            
            
                this.Owner_AICConversations = new BindingList<AICConversation>();
            
                this.OwnerEmailAddress_AICConversations = new BindingList<AICConversation>();
            
                this.Auth0SID_AICConversations = new BindingList<AICConversation>();
            
                this.Owner_AICProjects = new BindingList<AICProject>();
            
                this.OwnerEmailAddress_AICProjects = new BindingList<AICProject>();
            
                this.Auth0SID_AICProjects = new BindingList<AICProject>();
            

        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AppUserId")]
        public String AppUserId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Notes")]
        public String Notes { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Assignee")]
        public String Assignee { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "EmailAddress")]
        public String EmailAddress { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Auth0SID")]
        public String Auth0SID { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Roles")]
        public String[] Roles { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICProjects")]
        public String[] AICProjects { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICConversations")]
        public String[] AICConversations { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Owner_AICConversations")]
        public BindingList<AICConversation> Owner_AICConversations { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OwnerEmailAddress_AICConversations")]
        public BindingList<AICConversation> OwnerEmailAddress_AICConversations { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Auth0SID_AICConversations")]
        public BindingList<AICConversation> Auth0SID_AICConversations { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Owner_AICProjects")]
        public BindingList<AICProject> Owner_AICProjects { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OwnerEmailAddress_AICProjects")]
        public BindingList<AICProject> OwnerEmailAddress_AICProjects { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Auth0SID_AICProjects")]
        public BindingList<AICProject> Auth0SID_AICProjects { get; set; }
            

        
        
        
    }
}
