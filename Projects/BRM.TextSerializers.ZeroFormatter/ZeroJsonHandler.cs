using System.Text;
using BRM.TextSerializers.Interfaces;
using ZeroFormatter;

namespace BRM.TextSerializers
{
    public sealed class ZeroJsonHandler : ISerializeText
    {
        public T AsObject<T>(string json)
        {
            var bytes = Encoding.UTF8.GetBytes(json);
            var obj = ZeroFormatterSerializer.Deserialize<T>(bytes);
            return obj;
        }

        /// <summary>
        /// Pretty not supported
        /// </summary>        
        public string AsString<T>(T serializableInstance, bool prettyPrint=true)
        {
            var bytes = ZeroFormatterSerializer.Serialize(serializableInstance);
            var json = Encoding.UTF8.GetString(bytes);
            return json;
        }

        public SerializationType SerializationType => SerializationType.Json;
    }
}
