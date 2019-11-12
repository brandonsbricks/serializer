namespace BRM.DataSerializers.Interfaces
{
    public interface ISerializeData
    {
        /// <summary>
        /// The implementation of instance determines the serialization formatting
        /// </summary>
        SerializationType SerializationType { get; }
    }


    public enum SerializationType : byte
    {
        Json,
        Xml,
        Binary,
    }
}