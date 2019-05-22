namespace BRM.TextSerializers.Interfaces
{
    public interface ISerializeText
    {
        /// <summary>
        /// Serializes object to string
        /// </summary>
        string AsString<T>(T serializableInstance, bool prettyPrint=true);
        
        /// <summary>
        /// Deserializes string to object 
        /// </summary>
        T AsObject<T>(string text);
        
        /// <summary>
        /// The implementation of instance determines the serialization formatting
        /// </summary>
        SerializationType SerializationType { get; }
    }
    
    public enum SerializationType : byte
    {
        Json,
        Xml,
    }
}
