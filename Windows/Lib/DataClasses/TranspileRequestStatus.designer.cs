using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspileRequestStatus 
    {
        private void InitPoco()
        {
            
            this.TranspileRequestStatusId = Guid.NewGuid();
            
            this.TranspileRequests = new BindingList<TranspileRequest>();
        }
        
        public Guid TranspileRequestStatusId { get; set; }
    
        public String Name { get; set; }
    
        public String Description { get; set; }
    
        public Int32 SortOrder { get; set; }
    
        
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
    }
}