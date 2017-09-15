using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class PlatformCategory 
    {
        private void InitPoco()
        {
            
            this.PlatformCategoryId = Guid.NewGuid();
            
            this.TranspilerPlatforms = new BindingList<TranspilerPlatform>();
        }
        
        public Guid PlatformCategoryId { get; set; }
    
        public String Name { get; set; }
    
        public Int32 SortOrder { get; set; }
    
        public String DescriptionMD { get; set; }
    
        public String DisplayName { get; set; }
    
        public Nullable<DateTime> CreatedOn { get; set; }
    
        public Boolean IsActive { get; set; }
    
        public String LowerName { get; set; }
    
        public String UpperName { get; set; }
    
        public String LowerHyphenName { get; set; }
    
        
        public BindingList<TranspilerPlatform> TranspilerPlatforms { get; set; }
    }
}