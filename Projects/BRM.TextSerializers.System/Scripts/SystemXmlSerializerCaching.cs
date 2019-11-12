using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using BRM.DataSerializers.Interfaces;

namespace BRM.TextSerializers
{
    public sealed class SystemXmlSerializerCaching : ISerializeText
    {
        public SerializationType SerializationType => SerializationType.Xml;
        private static Dictionary<Type, XmlSerializer> _serializerCache = new Dictionary<Type, XmlSerializer>();
        
        public void CacheSerializer<T>()
        {
            XmlSerializer serializer;
            if (!_serializerCache.TryGetValue(typeof(T), out serializer))
            {
                _serializerCache.Add(typeof(T), new XmlSerializer(typeof(T)));
            }
        }

        public void ClearSerializerCache()
        {
            _serializerCache.Clear();
        }

        public T AsObject<T>(string xmlString)
        {
            CacheSerializer<T>();
            T obj = default(T);
            XmlSerializer serializer = _serializerCache[typeof(T)];
            var doc = new XmlDocument();
            doc.LoadXml(xmlString);
            using (var reader = new XmlNodeReader(doc))
            {
                obj = (T)serializer.Deserialize(reader);
            }
            return obj;
        }

        public string AsString<T>(T serializableInstance, bool prettyPrint = false)
        {
            CacheSerializer<T>();
            XmlSerializer serializer = _serializerCache[typeof(T)];
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, serializableInstance);
                return stringWriter.ToString();
            }
        }
    }
}