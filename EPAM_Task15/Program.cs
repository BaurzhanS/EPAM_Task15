using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM_Task15.Task1;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace EPAM_Task15
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"source.txt");

            string destination = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"destination.txt");

            Console.WriteLine($"ByteCopy() done. Total bytes: {FileManagement.ByByteCopy(source, destination)}");

            Console.WriteLine($"InMemoryByteCopy() done. Total bytes: {FileManagement.InMemoryByByteCopy(source, destination)}");

            Console.WriteLine($"ByBlockCopy() done. Total bytes: {FileManagement.ByBlockCopy(source, destination)}");

            Console.WriteLine($"InMemoryByBlockCopy() done. Total bytes: {FileManagement.InMemoryByBlockCopy(source, destination)}");

            Console.WriteLine($"BufferedCopy() done. Total bytes: {FileManagement.BufferedCopy(source, destination)}");

            Console.WriteLine($"ByLineCopy() done. Total bytes: {FileManagement.ByLineCopy(source, destination)}");

            Console.WriteLine(FileManagement.IsContentEquals(source, destination));
        }
    }
}
