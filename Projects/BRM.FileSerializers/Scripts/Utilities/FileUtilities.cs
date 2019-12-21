using System.IO;

namespace BRM.FileSerializers
{
    public static class FileUtilities
    {
        /// <summary>
        /// returns the first unique file path, using numeric incrementation
        /// <para>eg: D:/myFile.txt already exists, so passing this file path will return D:/myFile1.txt</para>  
        /// </summary>
        public static string GetUniqueFilePath(string filePath)
        {
            int i = 0;
            string uniquePath = filePath;
            while (File.Exists(uniquePath))
            {
                string directory = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string fileExtension = Path.GetExtension(filePath);
                uniquePath = $"{directory}/{fileName}{i}{fileExtension}";
                i++;
            }
            return uniquePath;
        }
        
        public static void SafeCreateDirectory(string filePath)
        {
            var directoryName = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }
    }
}