using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class Transpiler 
    {
        public Transpiler()
        {
            this.InitPoco();
        }

        public override string ToString()
        {
            return this.DisplayName;
        }

    }
}