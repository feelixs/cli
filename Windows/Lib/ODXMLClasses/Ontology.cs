using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SSoTme.OST.ODXMLClasses
{
    [Serializable]
    public class Ontology
    {
        public Ontology()
        {
            this.OntologyGroups = new OntologyGroup[] { };
        }

        [XmlAttribute]
        public Guid Id { get; set; }

        public String Name { get; set; }
        public String Namespace { get; set; }

        public OntologyGroup[] OntologyGroups { get; set; }

        public static Ontology Load(String fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Ontology));
            FileStream fs = new FileStream(fileName, FileMode.Open);
            try
            {
                return ser.Deserialize(fs) as Ontology;
            }
            finally
            {
                fs.Close();
            }
        }

        public void Save(String fileName)
        {
            String dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            XmlSerializer ser = new XmlSerializer(typeof(Ontology));
            FileStream fs = new FileStream(fileName, FileMode.Create);
            try
            {
                ser.Serialize(fs, this);
            }
            finally
            {
                fs.Close();
            }

        }
    }
}
