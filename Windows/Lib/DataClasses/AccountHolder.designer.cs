using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class AccountHolder 
    {
        private void InitPoco()
        {
            
            this.AccountHolderId = Guid.NewGuid();
            
            this.Transpilers = new BindingList<Transpiler>();
            this.TranspileRequests = new BindingList<TranspileRequest>();
            this.TranspilerHosts = new BindingList<TranspilerHost>();
        }
        
        public Guid AccountHolderId { get; set; }
    
        public String Name { get; set; }
    
        public String EmailAddress { get; set; }
    
        public String HashOfSecret { get; set; }
    
        public String ScreenName { get; set; }
    
        
        public BindingList<Transpiler> Transpilers { get; set; }
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
        public BindingList<TranspilerHost> TranspilerHosts { get; set; }
    }
}