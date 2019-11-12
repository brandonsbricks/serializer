using BRM.DataSerializers.Interfaces;
using Newtonsoft.Json;

namespace BRM.TextSerializers
{
    public sealed class NewtonsoftJsonSerializer : ISerializeText
    {
        JsonSerializerSettings _settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public T AsObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string AsString<T>(T serializableInstance, bool prettyPrint = true)
        {
            Formatting formatting = prettyPrint ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(serializableInstance, formatting, _settings);
        }
        public SerializationType SerializationType => SerializationType.Json;
    }
}
