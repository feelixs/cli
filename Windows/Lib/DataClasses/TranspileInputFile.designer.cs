using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspileInputFile 
    {
        private void InitPoco()
        {
            
            this.TranspileInputFileId = Guid.NewGuid();
            
        }
        
        public Guid TranspileInputFileId { get; set; }
    
        public Guid TranspileRequestId { get; set; }
    
        public Guid TranspileFileId { get; set; }
    
        
    }
}