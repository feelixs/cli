using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspileRequest 
    {
        private void InitPoco()
        {
            
            this.TranspileRequestId = Guid.NewGuid();
            
            this.ProjectTranspilers = new BindingList<ProjectTranspiler>();
            this.TranspileInputFiles = new BindingList<TranspileInputFile>();
            this.TranspileOutputFiles = new BindingList<TranspileOutputFile>();
        }
        
        public Guid TranspileRequestId { get; set; }
    
        public Guid TranspileRequestStatusId { get; set; }
    
        public DateTime CreatedOn { get; set; }
    
        public Guid TranspilerId { get; set; }
    
        public Guid AccountHolderId { get; set; }
    
        public Byte[] ZippedInputFileSet { get; set; }
    
        public Byte[] ZippedOutputFileSet { get; set; }
    
        public Nullable<Guid> TranspilerInstanceId { get; set; }
    
        
        public BindingList<ProjectTranspiler> ProjectTranspilers { get; set; }
        public BindingList<TranspileInputFile> TranspileInputFiles { get; set; }
        public BindingList<TranspileOutputFile> TranspileOutputFiles { get; set; }
    }
}