using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BRM.DataSerializers.Interfaces;

namespace BRM.BinarySerializers
{
    public class SystemBinarySerializer : ISerializeBinary
    {
        public SerializationType SerializationType => SerializationType.Binary;
        
        public byte[] AsBinary<T>(T serializableInstance)
        {
            using (var memStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memStream, serializableInstance);
                return memStream.ToArray();
            }
        }

        public T AsObject<T>(byte[] data)
        {
            using (var memStream = new MemoryStream(data))
            {
                memStream.Position = 0;
                var binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(memStream);
            }
        }
    }
}