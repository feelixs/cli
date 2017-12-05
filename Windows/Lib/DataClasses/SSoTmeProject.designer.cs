using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class SSoTmeProject
    {
        private void InitPoco()
        {
            
            this.SSoTmeProjectId = Guid.NewGuid();
            
                this.ProjectSettings = new BindingList<ProjectSetting>();
            
                this.ProjectTranspilers = new BindingList<ProjectTranspiler>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SSoTmeProjectId")]
        public Guid SSoTmeProjectId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Description")]
        public String Description { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public Nullable<DateTime> CreatedOn { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RootPath")]
        public String RootPath { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectSettings")]
        public BindingList<ProjectSetting> ProjectSettings { get; set; }
            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectTranspilers")]
        public BindingList<ProjectTranspiler> ProjectTranspilers { get; set; }
            

        

        private static string CreateSSoTmeProjectWhere(IEnumerable<SSoTmeProject> sSoTmeProjects)
        {
            if (!sSoTmeProjects.Any()) return "1=1";
            else 
            {
                var idList = sSoTmeProjects.Select(selectSSoTmeProject => String.Format("'{0}'", selectSSoTmeProject.SSoTmeProjectId));
                var csIdList = String.Join(",", idList);
                return String.Format("SSoTmeProjectId in ({0})", csIdList);
            }
        }
        
    }
}