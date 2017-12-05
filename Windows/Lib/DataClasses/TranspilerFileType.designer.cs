using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerFileType
    {
        private void InitPoco()
        {
            
            this.TranspilerFileTypeId = Guid.NewGuid();
            
                this.TranspilerFiles = new BindingList<TranspilerFile>();
            

        }

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerFileTypeId")]
        public Guid TranspilerFileTypeId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "DisplayName")]
        public String DisplayName { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "MimeType")]
        public String MimeType { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CommonExtension")]
        public String CommonExtension { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TranspilerFiles")]
        public BindingList<TranspilerFile> TranspilerFiles { get; set; }
            

        

        private static string CreateTranspilerFileTypeWhere(IEnumerable<TranspilerFileType> transpilerFileTypes)
        {
            if (!transpilerFileTypes.Any()) return "1=1";
            else 
            {
                var idList = transpilerFileTypes.Select(selectTranspilerFileType => String.Format("'{0}'", selectTranspilerFileType.TranspilerFileTypeId));
                var csIdList = String.Join(",", idList);
                return String.Format("TranspilerFileTypeId in ({0})", csIdList);
            }
        }
        
    }
}