using System.Collections.Generic;
using BRM.DataSerializers.Interfaces;

namespace BRM.FileSerializers
{
    public static class FileValidator
    {
        public static readonly Dictionary<SerializationType, string> ValidTypes = new Dictionary<SerializationType, string>()
        {
            {SerializationType.Json, ".json, .txt, .rtf"},
            {SerializationType.Xml, ".xml, .txt, .rtf"},
        };
        public static bool IsValidExtensionForType(SerializationType type, string extension)
        {
            bool isNull = string.IsNullOrEmpty(extension);
            bool isValid = !isNull && ValidTypes[type].Contains(extension);
            return isValid;
        }
    }
}
