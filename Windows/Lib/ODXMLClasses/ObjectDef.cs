using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSoTme.OST.ODXMLClasses
{
    public class ObjectDef
    {
        public ObjectDef()
        {
            this.PropertyDefs = new PropertyDef[] { };
            this.AttributeNames = new string[] { };
        }

        public String Name;

        public String Namespace;

        public PropertyDef[] PropertyDefs;

        public String[] AttributeNames;
    }
}
