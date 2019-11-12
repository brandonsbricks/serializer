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
        
        public TModel Read<TModel>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _debugger.LogErrorFormat("No file found at the filePath:{0}", filePath);
                return default;
            }
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                return _serializer.AsObject<TModel>(data);
            }
        }

        public void Write<TModel>(string filePath, TModel model)
        {
            SafeCreateDirectory(filePath);
            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                var data = _serializer.AsBinary(model);
                stream.Write(data, 0, data.Length);
            }
        }

        private void SafeCreateDirectory(string filePath)
        {
            var directoryName = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }
    }
}