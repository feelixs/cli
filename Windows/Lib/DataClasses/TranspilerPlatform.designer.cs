using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerPlatform 
    {
        private void InitPoco()
        {
            
            this.TranspilerPlatformId = Guid.NewGuid();
            
            this.Transpilers = new BindingList<Transpiler>();
        }
        
        public Guid TranspilerPlatformId { get; set; }
    
        public Guid PlatformCategoryId { get; set; }
    
        public String Name { get; set; }
    
        public String DescriptionMD { get; set; }
    
        public String DisplayName { get; set; }
    
        public Nullable<DateTime> CreatedOn { get; set; }
    
        public Boolean IsActive { get; set; }
    
        public String LowerName { get; set; }
    
        public String UpperName { get; set; }
    
        public String LowerHyphenName { get; set; }
    
        
        public BindingList<Transpiler> Transpilers { get; set; }
    }
}