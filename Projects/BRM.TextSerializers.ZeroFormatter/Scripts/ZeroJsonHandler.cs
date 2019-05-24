using System.Text;
using BRM.TextSerializers.Interfaces;
using ZeroFormatter;

namespace BRM.TextSerializers
{
    public sealed class ZeroJsonHandler : ISerializeText
    {
        public Encoding Encoding = Encoding.UTF8;
        public T AsObject<T>(string json)
        {
            var bytes = Encoding.GetBytes(json);
            var obj = ZeroFormatterSerializer.Deserialize<T>(bytes);
            return obj;
        }

        /// <summary>
        /// Pretty not supported
        /// </summary>        
        public string AsString<T>(T serializableInstance, bool prettyPrint = true)
        {
            var bytes = ZeroFormatterSerializer.Serialize(serializableInstance);
            var json = Encoding.GetString(bytes);
            return json;
        }

        public SerializationType SerializationType => SerializationType.Json;
    }
}
