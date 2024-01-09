using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CRM.Data
{
    public class LogSchemaUtility
    {
        public const string SchemaNamespace = "http://tempuri.org/LogSchema.xsd";

        public static string Serialize<T>(object o, bool indented)
        {
            StringWriter writer = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(writer);
            if (indented)
            {
                xmlWriter.Formatting = Formatting.Indented;
            }
            else
            {
                xmlWriter.Formatting = Formatting.None;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(xmlWriter, o);
            return writer.ToString();
        }

        public static XmlDocument Serialize(object o, bool indented = false)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty);
            xmlDoc.PrependChild(xmlDec);
            XmlElement elemRoot = xmlDoc.CreateElement(o.GetType().Name);
            xmlDoc.AppendChild(elemRoot);
            XmlElement elem = null;
            Type idType = o.GetType();
            List<string> validTypes = new List<string>() { "DateTime", "Int32", "Char", "String", "Int64", "Int", "String", "Byte" };
            List<System.Reflection.PropertyInfo> props = idType.GetProperties().Where(t => validTypes.Contains(t.PropertyType.Name.ToString())).ToList();


            foreach (System.Reflection.PropertyInfo pInfo in props)
            {
                //if (ignoredList !=null && ignoredList.Count() > 0 && ignoredList.Contains(pInfo.Name))
                //    continue;

                elem = xmlDoc.CreateElement(pInfo.Name);
                var val = pInfo.GetValue(o, null);
                elem.InnerText = val == null ? "" : val.ToString();
                elemRoot.AppendChild(elem);
            }

            return xmlDoc;
        }

        public static string Serialize(object obj)
        {
            StringWriter writer = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(writer);
            xmlWriter.Formatting = Formatting.Indented;
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(xmlWriter, obj);
            return writer.ToString();
        }


        public static T Deserialize<T>(string objectXml)
        {
                StringReader reader = new StringReader(objectXml);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T classObject = ((T)(serializer.Deserialize(reader)));
                reader.Close();
                return classObject;
        }

        public static T DeserializeFile<T>(string path)
        {
            string xml = File.ReadAllText(path);
            return LogSchemaUtility.Deserialize<T>(xml);
        }


    }
}
