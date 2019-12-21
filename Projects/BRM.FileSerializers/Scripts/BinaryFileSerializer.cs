using System.IO;
using BRM.DataSerializers.Interfaces;
using BRM.DebugAdapter.Interfaces;
using BRM.FileSerializers.Interfaces;

namespace BRM.FileSerializers
{
    public class BinaryFileSerializer : IReadFiles, IWriteFiles
    {
        private readonly ISerializeBinary _serializer;
        private readonly IDebug _debugger;

        public BinaryFileSerializer(ISerializeBinary serializer, IDebug debugger)
        {
            _serializer = serializer;
            _debugger = debugger;
        }
        
        /// <summary>
        /// If no file exists at <paramref name="filePath"/>, the default value for <typeparamref name="TModel"/> is used
        /// <para>
        /// Otherwise, opens the file located at <paramref name="filePath"/> and deserializes the data to type <typeparamref name="TModel"/>
        /// </para>
        /// </summary>
        public TModel Read<TModel>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _debugger.LogWarningFormat("No file found at the filePath:{0}", filePath);
                return default;
            }
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                return _serializer.AsObject<TModel>(data);
            }
        }

        /// <summary>
        /// Serializes <paramref name="model"/> to binary and writes to <paramref name="filePath"/>
        /// <para>
        /// Creates the containing directory if none already exists for <paramref name="filePath"/>
        /// </para>
        /// </summary>
        public void Write<TModel>(string filePath, TModel model)
        {
            FileUtilities.SafeCreateDirectory(filePath);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var data = _serializer.AsBinary(model);
                stream.Write(data, 0, data.Length);
            }
        }
    }
}