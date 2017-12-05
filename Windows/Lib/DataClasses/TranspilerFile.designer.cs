using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerFile
    {
        private void InitPoco()
        {
            
            this.TranspilerFileId = Guid.NewGuid();
            
                this.TranspileFileId_TranspileInputFiles = new BindingList<TranspileInputFile>();
            
                this.TranspileFileId_TranspileOutputFiles = new BindingList<TranspileOutputFile>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerFileId")]
        public Guid TranspilerFileId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerFileTypeId")]
        public Guid TranspilerFileTypeId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ZippedBytes")]
        public Byte[] ZippedBytes { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileFileId_TranspileInputFiles")]
        public BindingList<TranspileInputFile> TranspileFileId_TranspileInputFiles { get; set; }
            
        /// <summary>
        /// Find the related TranspileInputFiles (from the list provided) and attach them locally to the TranspileInputFiles list.
        /// </summary>
        public void LoadTranspileInputFiles(IEnumerable<TranspileInputFile> transpileInputFiles)
        {
            transpileInputFiles.Where(whereTranspileInputFile => whereTranspileInputFile.TranspileFileId == this.TranspilerFileId)
                    .ToList()
                    .ForEach(feTranspileInputFile => this.TranspileFileId_TranspileInputFiles.Add(feTranspileInputFile));
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspileFileId_TranspileOutputFiles")]
        public BindingList<TranspileOutputFile> TranspileFileId_TranspileOutputFiles { get; set; }
            
        /// <summary>
        /// Find the related TranspileOutputFiles (from the list provided) and attach them locally to the TranspileOutputFiles list.
        /// </summary>
        public void LoadTranspileOutputFiles(IEnumerable<TranspileOutputFile> transpileOutputFiles)
        {
            transpileOutputFiles.Where(whereTranspileOutputFile => whereTranspileOutputFile.TranspileFileId == this.TranspilerFileId)
                    .ToList()
                    .ForEach(feTranspileOutputFile => this.TranspileFileId_TranspileOutputFiles.Add(feTranspileOutputFile));
        }
        

        

        private static string CreateTranspilerFileWhere(IEnumerable<TranspilerFile> transpilerFiles)
        {
            if (!transpilerFiles.Any()) return "1=1";
            else 
            {
                var idList = transpilerFiles.Select(selectTranspilerFile => String.Format("'{0}'", selectTranspilerFile.TranspilerFileId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspilerFileId in ({0})", csIdList);
            }
        }
        
    }
}