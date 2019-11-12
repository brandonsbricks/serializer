namespace BRM.DataSerializers.Interfaces
{
    public interface ISerializeBinary : ISerializeData
    {
        /// <summary>
        /// Serializes object to string
        /// </summary>
        byte[] AsBinary<T>(T serializableInstance);
        
        /// <summary>
        /// Deserializes binary to object 
        /// </summary>
        T AsObject<T>(byte[] data);
    }
}