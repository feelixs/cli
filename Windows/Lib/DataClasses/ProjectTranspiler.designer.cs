using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class ProjectTranspiler
    {
        private void InitPoco()
        {
            
            this.ProjectTranspilerId = Guid.NewGuid();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectTranspilerId")]
        public Guid ProjectTranspilerId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SSoTmeProjectId")]
        public Guid SSoTmeProjectId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RelativePath")]
        public String RelativePath { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CommandLine")]
        public String CommandLine { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public Nullable<DateTime> CreatedOn { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LastTranspilerRequestId")]
        public Guid LastTranspilerRequestId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsDisabled")]
        public Boolean IsDisabled { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SortOrder")]
        public Int32 SortOrder { get; set; }
    

        

        

        private static string CreateProjectTranspilerWhere(IEnumerable<ProjectTranspiler> projectTranspilers)
        {
            if (!projectTranspilers.Any()) return "1=1";
            else 
            {
                var idList = projectTranspilers.Select(selectProjectTranspiler => String.Format("'{0}'", selectProjectTranspiler.ProjectTranspilerId));
                var csIdList = String.Join(",", idList);
                return String.Format("ProjectTranspilerId in ({0})", csIdList);
            }
        }
        
    }
}