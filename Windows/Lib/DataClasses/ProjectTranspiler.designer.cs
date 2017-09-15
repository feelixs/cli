using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class ProjectTranspiler 
    {
        private void InitPoco()
        {
            
            this.ProjectTranspilerId = Guid.NewGuid();
            
        }
        
        public Guid ProjectTranspilerId { get; set; }
    
        public Guid SSoTmeProjectId { get; set; }
    
        public String Name { get; set; }
    
        public String RelativePath { get; set; }
    
        public String CommandLine { get; set; }
    
        public Nullable<DateTime> CreatedOn { get; set; }
    
        public Guid LastTranspilerRequestId { get; set; }
    
        public Boolean IsDisabled { get; set; }
    
        public Int32 SortOrder { get; set; }
    
        
    }
}