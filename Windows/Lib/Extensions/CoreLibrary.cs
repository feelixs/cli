using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Extensions
{
    public static class CoreLibrary
    {
        public static String SafeToString(this object value)
        {
            if (ReferenceEquals(value, null)) return string.Empty;
            else return value.ToString();
        }
    }
}
