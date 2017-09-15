using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class TranspilerHost 
    {
        private void InitPoco()
        {
            
            this.TranspilerHostId = Guid.NewGuid();
            
            this.TranspilerInstances = new BindingList<TranspilerInstance>();
        }
        
        public Guid TranspilerHostId { get; set; }
    
        public Guid AccountHolderId { get; set; }
    
        public String BaseRoutingKey { get; set; }
    
        public Int32 TranspilerHostIndex { get; set; }
    
        public DateTime CreatedOn { get; set; }
    
        public Nullable<DateTime> LastPing { get; set; }
    
        public Int32 TimeoutSeconds { get; set; }
    
        public Boolean IsTerminated { get; set; }
    
        public Nullable<DateTime> TermatedOn { get; set; }
    
        
        public BindingList<TranspilerInstance> TranspilerInstances { get; set; }
    }
}