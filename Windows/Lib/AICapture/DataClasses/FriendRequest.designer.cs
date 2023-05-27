using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace AIC.Lib.DataClasses
{                            
    public partial class FriendRequest
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FriendRequestId")]
        public String FriendRequestId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Notes")]
        public String Notes { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICUser")]
        [RemoteIsCollection]
        public String AICUser { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICaptureUserId")]
        [RemoteIsCollection]
        public String AICaptureUserId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RequestedFriend")]
        [RemoteIsCollection]
        public String RequestedFriend { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RequestedFriendAICaptureUserId")]
        [RemoteIsCollection]
        public String RequestedFriendAICaptureUserId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FriendRequestAccepted")]
        public Nullable<Boolean> FriendRequestAccepted { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SharedDirectories")]
        [RemoteIsCollection]
        public String[] SharedDirectories { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FriendshipId")]
        public String FriendshipId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ReciprocalRequest")]
        [RemoteIsCollection]
        public String ReciprocalRequest { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ReciprocalRequestAccepted")]
        [RemoteIsCollection]
        public Nullable<Boolean> ReciprocalRequestAccepted { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsBiDirectionalFriendship")]
        public Nullable<Int32> IsBiDirectionalFriendship { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentRequest")]
        [RemoteIsCollection]
        public String ParentRequest { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentRequestAccepted")]
        [RemoteIsCollection]
        public Nullable<Boolean> ParentRequestAccepted { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICaptureUserName")]
        [RemoteIsCollection]
        public String AICaptureUserName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RequestedFriendAICaptureUserName")]
        [RemoteIsCollection]
        public String RequestedFriendAICaptureUserName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RequestSentToEmailAddress")]
        public String RequestSentToEmailAddress { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RequestStatus")]
        public String RequestStatus { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "FriendRequestRejected")]
        public Nullable<Boolean> FriendRequestRejected { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RequestTarget")]
        public String RequestTarget { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ReciprocalRequestRejected")]
        [RemoteIsCollection]
        public Nullable<Boolean> ReciprocalRequestRejected { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ParentRequestRejected")]
        [RemoteIsCollection]
        public Nullable<Boolean> ParentRequestRejected { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RelativeRequestUrl")]
        public String RelativeRequestUrl { get; set; }
    

        

        
        
        
    }
}
