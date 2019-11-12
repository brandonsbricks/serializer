namespace BRM.DataSerializers.Interfaces
{
    public interface ISerializeText : ISerializeData
    {
        /// <summary>
        /// Serializes object to string
        /// </summary>
        string AsString<T>(T serializableInstance, bool prettyPrint=true);
        
        /// <summary>
        /// Deserializes string to object 
        /// </summary>
        T AsObject<T>(string text);
    }
}
