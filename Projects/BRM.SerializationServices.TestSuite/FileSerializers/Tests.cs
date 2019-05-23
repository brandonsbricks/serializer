using System;
using System.IO;
using BRM.DebugAdapter;
using BRM.DebugAdapter.Interfaces;
using BRM.FileSerializers;
using BRM.TextSerializers;
using BRM.TextSerializers.Interfaces;
using Xunit;

namespace BRM.SerializationServices.TestSuite
{
    public class Tests
    {
        [Theory]
        [InlineData("test1.json", "")]
        [InlineData("test1.json", "value")]
        public void FileExists(string relativePath, string toSerialize)
        {
            //setup
            IDebug debugger = new ConsoleDebugger();
            ISerializeText jsonSerializer = new NewtonsoftJsonHandler();
            FileSerializer serializer = new FileSerializer(jsonSerializer, debugger);
            var finalPath = Path.Combine(Environment.CurrentDirectory, relativePath);
            if (File.Exists(finalPath))
            {
                File.Delete(finalPath);
            }

            //write / read
            serializer.Write(finalPath, toSerialize);
            Assert.True(File.Exists(finalPath));
            var sourceString = jsonSerializer.AsString(toSerialize);
            var deserialized = serializer.Read<string>(finalPath);
            var comparisonString = jsonSerializer.AsString(deserialized);
            
            //compare results
            Assert.Equal(sourceString, comparisonString);
            
            //cleanup
            File.Delete(finalPath);
        }
    }
}