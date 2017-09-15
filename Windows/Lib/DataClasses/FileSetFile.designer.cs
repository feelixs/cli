using System;
using System.ComponentModel;
                        
namespace SSoTme.Default.Lib
{                            
    public partial class FileSetFile 
    {
        private void InitPoco()
        {
            
            this.FileSetFileId = Guid.NewGuid();
            
        }
        
        public Guid FileSetFileId { get; set; }
    
        public Guid FileSetId { get; set; }
    
        public String RelativePath { get; set; }
    
        public String FileContents { get; set; }
    
        public Byte[] ZippedFileContents { get; set; }
    
        public Boolean AlwaysOverwrite { get; set; }
    
        public Boolean SkipClean { get; set; }
    
        
    }
}