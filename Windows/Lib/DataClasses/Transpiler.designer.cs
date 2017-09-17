using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class Transpiler 
    {
        private void InitPoco()
        {
            
            this.TranspilerId = Guid.NewGuid();
            
            this.TranspileRequests = new BindingList<TranspileRequest>();
            this.TranspilerInstances = new BindingList<TranspilerInstance>();
            this.TranspilerVersions = new BindingList<TranspilerVersion>();
        }
        
        public Guid TranspilerId { get; set; }
    
        public Guid AccountHolderId { get; set; }
    
        public Nullable<Guid> TranspilerPlatformId { get; set; }
    
        public String Name { get; set; }
    
        public String DisplayName { get; set; }
    
        public String Description { get; set; }
    
        public Nullable<DateTime> CreatedOn { get; set; }
    
        public Boolean IsActive { get; set; }
    
        public String CurrentRoutingKey { get; set; }
    
        public Boolean IsPrivate { get; set; }
    
        public String LowerName { get; set; }
    
        public String UpperName { get; set; }
    
        public String LowerHyphenName { get; set; }
    
        public String ReadMeMD { get; set; }
    
        public String InputDescriptionMD { get; set; }
    
        public String OutputDescriptionMD { get; set; }
    
        public String ExampleMD { get; set; }
    
        
        public BindingList<TranspileRequest> TranspileRequests { get; set; }
        public BindingList<TranspilerInstance> TranspilerInstances { get; set; }
        public BindingList<TranspilerVersion> TranspilerVersions { get; set; }
    }
}