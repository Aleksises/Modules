using Methods.Enums;
using Methods.FileSystem;
using System;

namespace Methods
{
    public class Program
    {
        private const string StartDirectory = "E:\\ConsoleApp1";

        public static void Main()
        {
            var visitor = new FileSystemVisitor(StartDirectory, new FileSystemProcessingStrategy(), (info) => true);

            visitor.Start += (s, e) =>
            {
                Console.WriteLine("Iteration started");
            };

            visitor.Finish += (s, e) =>
            {
                Console.WriteLine("Iteration finished");
            };

            visitor.FileFinded += (s, e) =>
            {
                Console.WriteLine("\tFounded file: " + e.FindedItem.Name);
            };

            visitor.DirectoryFinded += (s, e) =>
            {
                Console.WriteLine("\tFounded directory: " + e.FindedItem.Name);
                if (e.FindedItem.Name.Length == 4)
                {
                    e.ActionType = ActionType.StopSearch;
                }
            };

            visitor.FilteredFileFinded += (s, e) =>
            {
                Console.WriteLine("Founded filtered file: " + e.FindedItem.Name);
            };

            visitor.FilteredDirectoryFinded += (s, e) =>
            {
                Console.WriteLine("Founded filtered directory: " + e.FindedItem.Name);
                if (e.FindedItem.Name.Length == 4)
                    e.ActionType = ActionType.StopSearch;
            };

            foreach (var fileSysInfo in visitor.GetFileSystemInfos())
            {
                Console.WriteLine(fileSysInfo);
            }
        }
    }
}
