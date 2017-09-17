using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspileOutputFile 
    {
        private void InitPoco()
        {
            
            this.TranspileOutputFileId = Guid.NewGuid();
            
        }
        
        public Guid TranspileOutputFileId { get; set; }
    
        public Guid TranspileRequestId { get; set; }
    
        public Guid TranspileFileId { get; set; }
    
        
    }
}