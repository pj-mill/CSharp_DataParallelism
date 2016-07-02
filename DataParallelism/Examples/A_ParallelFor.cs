using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DataParallelism.Examples
{
    public class A_ParallelFor
    {
                
        public static void Run()
        {
            DirectoryExample();
        }

        /// <summary>
        /// Calculates the total size of files in a directory
        /// </summary>
        private static void DirectoryExample()
        {
            string path = @"C:\Users\Public\Pictures\Sample Pictures\";
            long totalSize = 0;

            // Check Path
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Path doe's not exist");
                return;
            }

            // Grab Files
            string[] files = Directory.GetFiles(path);

            // iterate through them and grab their data
            Parallel.For(0, files.Length, idx =>
            {
                FileInfo fi = new FileInfo(files[idx]);
                long size = fi.Length;
                Interlocked.Add(ref totalSize, size);
            });

            // Print totals
            Console.WriteLine($"Directory '{path}':");
            Console.WriteLine($"{files.Length:N0} files, {totalSize:N0} bytes");
        }
    }
}
