using System.IO;
using System.Xml;
using System.Xml.Serialization;
using BRM.DataSerializers.Interfaces;

namespace BRM.TextSerializers
{
    public sealed class SystemXmlSerializerLazy : ISerializeText
    {
        public SerializationType SerializationType => SerializationType.Xml;
        
        public T AsObject<T>(string xmlString)
        {
            T obj = default(T);
            var serializer = new XmlSerializer(typeof(T));
            var doc = new XmlDocument();
            doc.LoadXml(xmlString);
            using (var reader = new XmlNodeReader(doc))
            {
                obj= (T)serializer.Deserialize(reader);
            }
            return obj;
        }

        public string AsString<T>(T serializableInstance, bool prettyPrint = false)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, serializableInstance);
                return stringWriter.ToString();
            }
        }
    }
}
