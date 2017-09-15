using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class ProjectSetting 
    {
        private void InitPoco()
        {
            
            this.ProjectSettingId = Guid.NewGuid();
            
        }
        
        public Guid ProjectSettingId { get; set; }
    
        public Guid SSoTmeProjectId { get; set; }
    
        public String Name { get; set; }
    
        public String Value { get; set; }
    
        
    }
}