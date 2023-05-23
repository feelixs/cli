using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace AIC.Lib.DataClasses
{                            
    public partial class AICModelPricing
    {
        private void InitPoco()
        {
        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICModelPricingId")]
        public String AICModelPricingId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "DateCreated")]
        public Nullable<DateTime> DateCreated { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AIModel")]
        public String AIModel { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "PriceFor1MillionTokens")]
        public Nullable<decimal> PriceFor1MillionTokens { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "PricePer1kPromptToken")]
        public Nullable<decimal> PricePer1kPromptToken { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "PricePer1kResponseToken")]
        public Nullable<decimal> PricePer1kResponseToken { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "PricePerMessage")]
        public Nullable<decimal> PricePerMessage { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICMessages")]
        public String[] AICMessages { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AICSkillSteps")]
        public String[] AICSkillSteps { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AIModelPricePer1KPromptTokens")]
        public Nullable<decimal> AIModelPricePer1KPromptTokens { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ModelIsPublic")]
        public Nullable<Boolean> ModelIsPublic { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsPublic")]
        public Nullable<Int32> IsPublic { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AIModelAIProvider")]
        public String AIModelAIProvider { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ResaleOf")]
        public String ResaleOf { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ResaleOfAIModelPricePer1KPromptTokens")]
        public Nullable<decimal> ResaleOfAIModelPricePer1KPromptTokens { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ResaleOfPriceFor1MillionTokens")]
        public Nullable<decimal> ResaleOfPriceFor1MillionTokens { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ResaleOfPricePer1kPromptToken")]
        public Nullable<decimal> ResaleOfPricePer1kPromptToken { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ResaleOfAIModelName")]
        public String ResaleOfAIModelName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ResaleOfPricePer1kResponseToken")]
        public Nullable<decimal> ResaleOfPricePer1kResponseToken { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SuggestedPricePer1kResponseToken")]
        public Nullable<decimal> SuggestedPricePer1kResponseToken { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ResaleOffPricePerMessage")]
        public Nullable<decimal> ResaleOffPricePerMessage { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ResaleMarkUp")]
        public Nullable<decimal> ResaleMarkUp { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AIModelName")]
        public String AIModelName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SuggestedPricePer1kPromptToken")]
        public Nullable<decimal> SuggestedPricePer1kPromptToken { get; set; }
    

        

        
        
        
    }
}
