using System;
using System.IO;
using BRM.BinarySerializers;
using BRM.DebugAdapter;
using BRM.FileSerializers;
using BRM.TextSerializers;
using Xunit;

namespace BRM.SerializationServices.TestSuite
{
    public class Tests
    {
        [Theory]
        [InlineData("test1.json", "")]
        [InlineData("test1.json", "value")]
        public void JsonPrePostSerialization_AreEqual(string relativeFilePath, string toSerialize)
        {
            //setup
            var debugger = new ConsoleDebugger();
            var jsonSerializer = new NewtonsoftJsonSerializer();
            var fileSerializer = new TextFileSerializer(jsonSerializer, debugger);
            var finalPath = Path.Combine(Environment.CurrentDirectory, relativeFilePath);
            if (File.Exists(finalPath))
            {
                File.Delete(finalPath);
            }

            //write / read
            fileSerializer.Write(finalPath, toSerialize);
            Assert.True(File.Exists(finalPath));
            
            var deserialized = fileSerializer.Read<string>(finalPath);
            
            //compare results
            Assert.Equal(toSerialize, deserialized);
            
            //cleanup
            File.Delete(finalPath);
        }

        [Theory]
        [InlineData("test1.dat", new byte[]{0,1,0,1,1,1,001,0110})]
        [InlineData("test1.dat", new byte[]{0,1,0,1,1,1,001,0110,000000001,0100})]
        public void BinaryPrePostSerialization_AreEqual(string relativeFilePath, byte[] toSerialize)
        {
            //setup
            var debugger = new ConsoleDebugger();
            var binarySerializer = new SystemBinarySerializer();
            var fileSerializer = new BinaryFileSerializer(binarySerializer, debugger);
            var finalPath = Path.Combine(Environment.CurrentDirectory, relativeFilePath);
            if (File.Exists(finalPath))
            {
                File.Delete(finalPath);
            }

            //write / read
            fileSerializer.Write(finalPath, toSerialize);
            Assert.True(File.Exists(finalPath));
            
            var deserialized = fileSerializer.Read<byte[]>(finalPath);
            //compare results
            Assert.Equal(toSerialize, deserialized);
            
            //cleanup
            File.Delete(finalPath);
        }
    }
}