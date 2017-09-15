using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
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