using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class SSoTmeProject 
    {
        private void InitPoco()
        {
            
            this.SSoTmeProjectId = Guid.NewGuid();
            
            this.ProjectSettings = new BindingList<ProjectSetting>();
            this.ProjectTranspilers = new BindingList<ProjectTranspiler>();
        }
        
        public Guid SSoTmeProjectId { get; set; }
    
        public String Name { get; set; }
    
        public String Description { get; set; }
    
        public Nullable<DateTime> CreatedOn { get; set; }
    
        public String RootPath { get; set; }


        public BindingList<ProjectSetting> ProjectSettings { get; set; }
        public BindingList<ProjectTranspiler> ProjectTranspilers { get; set; }
    }
}