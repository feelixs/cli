using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerFile 
    {
        private void InitPoco()
        {
            
            this.TranspilerFileId = Guid.NewGuid();
            
            this.TranspileInputFiles = new BindingList<TranspileInputFile>();
            this.TranspileOutputFiles = new BindingList<TranspileOutputFile>();
        }
        
        public Guid TranspilerFileId { get; set; }
    
        public Guid TranspilerFileTypeId { get; set; }
    
        public String Name { get; set; }
    
        public Byte[] ZippedBytes { get; set; }
    
        
        public BindingList<TranspileInputFile> TranspileInputFiles { get; set; }
        public BindingList<TranspileOutputFile> TranspileOutputFiles { get; set; }
    }
}