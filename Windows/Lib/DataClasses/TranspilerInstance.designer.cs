using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class TranspilerInstance 
    {
        private void InitPoco()
        {
            
            this.TranspilerInstanceId = Guid.NewGuid();
            
            this.TranspileRequests = new BindingList<TranspileRequest>();
        }
        
        public Guid TranspilerInstanceId { get; set; }
    
        public Guid TranspilerHostId { get; set; }
    
        public Nullable<Guid> TranspilerId { get; set; }
    
        public Nullable<Guid> TranspilerVersionId { get; set; }
    
        public String RoutingKey { get; set; }
    
        public Boolean IsTerminated { get; set; }
    
        
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
    }
}