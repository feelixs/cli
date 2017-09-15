using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class TranspilerFileType 
    {
        private void InitPoco()
        {
            
            this.TranspilerFileTypeId = Guid.NewGuid();
            
            this.TranspilerFiles = new BindingList<TranspilerFile>();
        }
        
        public Guid TranspilerFileTypeId { get; set; }
    
        public String Name { get; set; }
    
        public String DisplayName { get; set; }
    
        public String MimeType { get; set; }
    
        public String CommonExtension { get; set; }
    
        
        public BindingList<TranspilerFile> TranspilerFiles { get; set; }
    }
}