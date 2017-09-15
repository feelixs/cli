using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class FileSet 
    {
        private void InitPoco()
        {
            
            this.FileSetId = Guid.NewGuid();
            
            this.FileSetFiles = new BindingList<FileSetFile>();
        }
        
        public Guid FileSetId { get; set; }
    
        public Nullable<DateTime> CreatedOn { get; set; }
    
        
        public BindingList<FileSetFile> FileSetFiles { get; set; }
    }
}