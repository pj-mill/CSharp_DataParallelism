using DataParallelism.Utilities;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DataParallelism.Examples
{
    public class A_ParallelFor
    {
                
        public static void Run()
        {
            PrintUtility.PrintTitle("PARALLEL FOR EXAMPLE");
            SimpleExample();
            DirectoryExample();
            ManipulateImageExample();
        }

        private static void SimpleExample()
        {
            PrintUtility.PrintSubTitle("SIMPLE EXAMPLE");
            Parallel.For(5, 10, idx => DoWork(idx));
        }

        /// <summary>
        /// Calculates the total size of files in a directory
        /// </summary>
        private static void DirectoryExample()
        {
            PrintUtility.PrintSubTitle("DIRECTORY EXAMPLE");

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


        private static void ManipulateImageExample()
        {
            PrintUtility.PrintSubTitle("TIMED BITMAP MANIPULATION EXAMPLE");

            // Vars
            Stopwatch stopWatch = new Stopwatch();
            PointF firstLocation = new PointF(10f, 10f);
            PointF secondLocation = new PointF(10f, 50f);

            // Get Image Files
            String[] files = Directory.GetFiles(@"C:\Users\Public\Pictures\Sample Pictures", "*.jpg");

            // Create new directoty
            String newDir = @"C:\Users\Public\Pictures\Sample Pictures\ModifiedFor";
            Directory.CreateDirectory(newDir);

            // Local Actions
            Func<string> divider = () => { return new String('-', 70); };
            Action<string> printTitleAction = (title) =>
            {
                Console.WriteLine(divider());
                Console.WriteLine(title);
                Console.WriteLine(divider());
            };
            Action<double> printTimeAction = (time) =>
            {
                Console.WriteLine(divider());
                Console.WriteLine($"Execution time = {time} seconds");
                Console.WriteLine(divider());
            };
            Action<Bitmap, string> manipulateImage = (file, fileName) =>
            {
                using(Graphics graphics = Graphics.FromImage(file))
                {
                    using(Font font = new Font("Arial", 10))
                    {
                        graphics.DrawString("Paul James", font, Brushes.Blue, firstLocation);
                        graphics.DrawString("Millar", font, Brushes.Red, secondLocation);
                    }
                }
                file.Save(Path.Combine(newDir, fileName));                
            };

            // Normal For
            printTitleAction("Manipulating images using for loop");
            stopWatch = Stopwatch.StartNew();
            for(int i = 0; i < files.Length; i++)
            {
                String filename = Path.GetFileName(files[i]);
                Bitmap bitmap = (Bitmap)Image.FromFile(files[i]);
                manipulateImage(bitmap, filename);
            }
            printTimeAction(stopWatch.Elapsed.TotalSeconds);

            // Parallel.For
            printTitleAction("Manipulating images using Parallel.For loop");
            stopWatch = Stopwatch.StartNew();
            Parallel.For(0, files.Length, (idx) =>
            {
                String filename = Path.GetFileName(files[idx]);
                Bitmap bitmap = (Bitmap)Image.FromFile(files[idx]);
                manipulateImage(bitmap, filename);
            });
            printTimeAction(stopWatch.Elapsed.TotalSeconds);
        }


        private static void DoWork(int num)
        {
            Console.WriteLine($"Product Of {num}: {num * num}");
        }
    }
}
