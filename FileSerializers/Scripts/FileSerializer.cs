using System.IO;
using BRM.DebugAdapter.Interfaces;
using BRM.FileSerializers.Interfaces;
using BRM.TextSerializers.Interfaces;

namespace BRM.FileSerializers
{
    /// <summary>
    /// Serializes/Deserializes text for disk files
    /// </summary>
    public class FileSerializer : IReadFiles, IWriteFiles
    {
        private ISerializeText _textHandler;
        private IDebug _debugger;

        /// <summary>
        /// textHandler must match the document type
        /// eg: jsonHandlers must be used for Reading/Writing .json files
        /// eg: xmlHandlers must be used for Reading/Writing .xml files
        /// </summary>
        public FileSerializer(ISerializeText textHandler, IDebug debugger)
        {
            _textHandler = textHandler;
            _debugger = debugger;
        }

        public TModel Read<TModel>(string filePath) where TModel : class
        {
            if (!IsValid(filePath, "read"))
            {
                return null;
            }
            var model = default(TModel);
            using (var stream = new FileStream(filePath, FileMode.Open))
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                model = _textHandler.AsObject<TModel>(json);
            }
            return model;
        }

        public void Write<TModel>(string filePath, TModel model) where TModel : class
        {
            if (!IsValid(filePath, "write"))
            {
                return;
            }
            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            using (var writer = new StreamWriter(stream, System.Text.Encoding.UTF8))
            {
                string json = _textHandler.AsString(model);
                writer.Write(json);
            }
        }

        private bool IsValid(string filePath, string operation)
        {
            string extension = Path.GetExtension(filePath);
            bool isValid = FileValidator.IsValidExtensionForType(_textHandler.SerializationType, extension);
            if (!isValid)
            {
                bool isExtensionNull = string.IsNullOrEmpty(extension);
                if (isExtensionNull)
                {
                    _debugger.LogWarningFormat("FileExtension is null");
                }
                else
                {
                    _debugger.LogWarningFormat("You may be attempting to {0} a file not supported by the member TextHandler. Type{1}, FileExtension:{2}", operation, _textHandler.SerializationType, extension);
                }
            }
            return isValid;
        }
    }
}