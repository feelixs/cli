using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SSoTme.OST.ODXMLClasses
{
    [Serializable]
    public class OntologyGroup
    {
        public OntologyGroup()
        {
            this.ObjectDefs = new ObjectDef[] { };
        }

        public String Name { get; set; }
        public String Namespace { get; set; }


        public ObjectDef[] ObjectDefs { get; set; }
    }
}
