using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSoTme.OST.ODXMLClasses
{
    public class PropertyDef
    {
        public PropertyDef()
        {
            this.AttributeNames = new string[] { };
        }
        public String Name;

        public string DataType;

        public string Namespace;

        public String[] AttributeNames;
    }
}
