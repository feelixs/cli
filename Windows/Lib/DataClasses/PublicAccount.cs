using SSoTme.OST.Lib.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoTme.OST.Lib.DataClasses
{
    public class PublicAccount
    {
        public PublicAccount()
        {
            this.Transpilers = new List<Transpiler>();
        }

        public Guid AccountHolderId { get; set; }
        public String ScreenName { get; set; }
        public List<Transpiler> Transpilers { get; set; }
    }
}
