using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspileRequest
    {
        private void InitPoco()
        {
            
            this.TranspileRequestId = Guid.NewGuid();
            
                this.LastTranspilerRequestId_ProjectTranspilers = new BindingList<ProjectTranspiler>();
            
                this.TranspileInputFiles = new BindingList<TranspileInputFile>();
            
                this.TranspileOutputFiles = new BindingList<TranspileOutputFile>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequestId")]
        public Guid TranspileRequestId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileRequestStatusId")]
        public Guid TranspileRequestStatusId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public DateTime CreatedOn { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerId")]
        public Guid TranspilerId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountHolderId")]
        public Guid AccountHolderId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ZippedInputFileSet")]
        public Byte[] ZippedInputFileSet { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ZippedOutputFileSet")]
        public Byte[] ZippedOutputFileSet { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerInstanceId")]
        public Nullable<Guid> TranspilerInstanceId { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LastTranspilerRequestId_ProjectTranspilers")]
        public BindingList<ProjectTranspiler> LastTranspilerRequestId_ProjectTranspilers { get; set; }
            
        /// <summary>
        /// Find the related ProjectTranspilers (from the list provided) and attach them locally to the ProjectTranspilers list.
        /// </summary>
        public void LoadProjectTranspilers(IEnumerable<ProjectTranspiler> projectTranspilers)
        {
            projectTranspilers.Where(whereProjectTranspiler => whereProjectTranspiler.LastTranspilerRequestId == this.TranspileRequestId)
                    .ToList()
                    .ForEach(feProjectTranspiler => this.LastTranspilerRequestId_ProjectTranspilers.Add(feProjectTranspiler));
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileInputFiles")]
        public BindingList<TranspileInputFile> TranspileInputFiles { get; set; }
            
        /// <summary>
        /// Find the related TranspileInputFiles (from the list provided) and attach them locally to the TranspileInputFiles list.
        /// </summary>
        public void LoadTranspileInputFiles(IEnumerable<TranspileInputFile> transpileInputFiles)
        {
            transpileInputFiles.Where(whereTranspileInputFile => whereTranspileInputFile.TranspileRequestId == this.TranspileRequestId)
                    .ToList()
                    .ForEach(feTranspileInputFile => this.TranspileInputFiles.Add(feTranspileInputFile));
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileOutputFiles")]
        public BindingList<TranspileOutputFile> TranspileOutputFiles { get; set; }
            
        /// <summary>
        /// Find the related TranspileOutputFiles (from the list provided) and attach them locally to the TranspileOutputFiles list.
        /// </summary>
        public void LoadTranspileOutputFiles(IEnumerable<TranspileOutputFile> transpileOutputFiles)
        {
            transpileOutputFiles.Where(whereTranspileOutputFile => whereTranspileOutputFile.TranspileRequestId == this.TranspileRequestId)
                    .ToList()
                    .ForEach(feTranspileOutputFile => this.TranspileOutputFiles.Add(feTranspileOutputFile));
        }
        

        

        private static string CreateTranspileRequestWhere(IEnumerable<TranspileRequest> transpileRequests)
        {
            if (!transpileRequests.Any()) return "1=1";
            else 
            {
                var idList = transpileRequests.Select(selectTranspileRequest => String.Format("'{0}'", selectTranspileRequest.TranspileRequestId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspileRequestId in ({0})", csIdList);
            }
        }
        
    }
}