using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Task15.Task1
{
    public static class FileManagement
    {
        public static int ByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            FileStream source = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
            byte[] byteSource = new byte[source.Length];
            source.Read(byteSource, 0, byteSource.Length);
            source.Dispose();

            FileStream destination = new FileStream(destinationPath, FileMode.OpenOrCreate, FileAccess.Write);
            destination.Write(byteSource, 0, byteSource.Length);
            int totalBytes = (int)destination.Length;
            destination.Dispose();
            return totalBytes;
        }

        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            TextReader reader = new StreamReader(sourcePath);
            byte[] block = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            reader.Dispose();
            MemoryStream memoryStream = new MemoryStream(block, 0, block.Length);
            memoryStream.Write(block, 0, block.Length);
            byte[] Array = memoryStream.ToArray();
            memoryStream.Dispose();
            char[] array = Encoding.UTF8.GetChars(Array);
            StreamWriter streamWriter = new StreamWriter(destinationPath);
            streamWriter.Write(array);
            int totalBytes = streamWriter.Encoding.GetByteCount(array);
            streamWriter.Close();
            return totalBytes;
        }

        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int totalBytes = 0;
            using (FileStream source = new FileStream(sourcePath, FileMode.Open))
            {
                byte[] block = new byte[source.Length];
                source.Read(block, 0, block.Length);
                using (FileStream destination = new FileStream(destinationPath, FileMode.Open))
                {
                    destination.Write(block, 0, block.Length);
                    totalBytes = (int)destination.Length;
                }
            }
            return totalBytes;
        }

        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            StreamReader reader = new StreamReader(sourcePath);
            byte[] block = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            reader.Dispose();
            int totalBytes = 0;
            using (MemoryStream memoryStream = new MemoryStream(block, 0, block.Length))
            {
                memoryStream.Write(block, 0, block.Length);
                byte[] Array = memoryStream.ToArray();
                Buffer.BlockCopy(block, 0, Array, 0, Array.Length);
                char[] chars = Encoding.UTF8.GetChars(Array);
                StreamWriter streamWriter = new StreamWriter(destinationPath);
                streamWriter.Write(chars);
                totalBytes = Array.Length;
                streamWriter.Close();
            }

            return totalBytes;
        }

        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);
            int totalBytes = 0;
            FileStream source = File.OpenRead(sourcePath);
            byte[] block = new byte[source.Length];
            source.Read(block, 0, block.Length);

            using (FileStream destination = new FileStream(destinationPath, FileMode.Open, FileAccess.Write))
            {
                using (BufferedStream buffer = new BufferedStream(destination, (int)source.Length))
                {
                    buffer.Write(block, 0, block.Length);
                    totalBytes = (int)destination.Length;
                }
            }
            return totalBytes;
        }

        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            string[] String = File.ReadAllLines(sourcePath);
            File.WriteAllLines(destinationPath, String);
            byte[] bytes = File.ReadAllBytes(destinationPath);
            return bytes.Length;
        }

        public static bool IsContentEquals(string sourcePath, string destinationPath)
        {
            bool resultOfEqual = File.ReadAllBytes(sourcePath).SequenceEqual(File.ReadAllBytes(destinationPath));
            return resultOfEqual;
        }

        private static void InputValidation(string sourcePath, string destinationPath)
        {
            if (sourcePath == null || destinationPath == null)
            {
                throw new ArgumentNullException("There is no such path!");
            }

            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException($"{(nameof(sourcePath))} doesn't found");
            }
        }

    }
}