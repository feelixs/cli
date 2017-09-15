using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class TranspilerInputHint 
    {
        private void InitPoco()
        {
            
            this.TranspilerInputHintId = Guid.NewGuid();
            
        }
        
        public Guid TranspilerInputHintId { get; set; }
    
        public Guid TranspilerVersionId { get; set; }
    
        public Nullable<Guid> TranspilerFileTypeId { get; set; }
    
        public String NameContains { get; set; }
    
        public Int32 SortOrder { get; set; }
    
        
    }
}