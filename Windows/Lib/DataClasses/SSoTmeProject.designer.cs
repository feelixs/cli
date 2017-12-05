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
            
        /// <summary>
        /// Find the related ProjectSettings (from the list provided) and attach them locally to the ProjectSettings list.
        /// </summary>
        public void LoadProjectSettings(IEnumerable<ProjectSetting> projectSettings)
        {
            projectSettings.Where(whereProjectSetting => whereProjectSetting.SSoTmeProjectId == this.SSoTmeProjectId)
                    .ToList()
                    .ForEach(feProjectSetting => this.ProjectSettings.Add(feProjectSetting));
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectTranspilers")]
        public BindingList<ProjectTranspiler> ProjectTranspilers { get; set; }
            
        /// <summary>
        /// Find the related ProjectTranspilers (from the list provided) and attach them locally to the ProjectTranspilers list.
        /// </summary>
        public void LoadProjectTranspilers(IEnumerable<ProjectTranspiler> projectTranspilers)
        {
            projectTranspilers.Where(whereProjectTranspiler => whereProjectTranspiler.SSoTmeProjectId == this.SSoTmeProjectId)
                    .ToList()
                    .ForEach(feProjectTranspiler => this.ProjectTranspilers.Add(feProjectTranspiler));
        }
        

        

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