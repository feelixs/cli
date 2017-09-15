using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class TranspilerVersion 
    {
        private void InitPoco()
        {
            
            this.TranspilerVersionId = Guid.NewGuid();
            
            this.TranspilerInputHints = new BindingList<TranspilerInputHint>();
            this.TranspilerInstances = new BindingList<TranspilerInstance>();
            this.TranspilerVersions = new BindingList<TranspilerVersion>();
        }
        
        public Guid TranspilerVersionId { get; set; }
    
        public Guid TranspilerId { get; set; }
    
        public String Name { get; set; }
    
        public String Description { get; set; }
    
        public Nullable<DateTime> CreatedOn { get; set; }
    
        public Boolean IsActive { get; set; }
    
        public Nullable<Guid> ReplacedByTranspilerVersionId { get; set; }
    
        
        public BindingList<TranspilerInputHint> TranspilerInputHints { get; set; }
        public BindingList<TranspilerInstance> TranspilerInstances { get; set; }
        public BindingList<TranspilerVersion> TranspilerVersions { get; set; }
    }
}