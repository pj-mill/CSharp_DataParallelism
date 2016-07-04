using DataParallelism.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace DataParallelism.Examples
{
    public class B_ParallelForEach
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("PARALLEL FOREACH");
            SimpleExample();
            TimedExample();
            ManipulateImageExample();
        }

        private static void SimpleExample()
        {
            PrintUtility.PrintSubTitle("SIMPLE EXAMPLE");

            int[] nums = Enumerable.Range(0, 10).ToArray();
            Parallel.ForEach(nums, (num) => { DoWork(num); });
        }

        private static void TimedExample()
        {
            PrintUtility.PrintSubTitle("TIMED EXAMPLE");

            List<string> fruits = new List<string>();
            fruits.Add("Apple");
            fruits.Add("Banana");
            fruits.Add("Bilberry");
            fruits.Add("Blackberry");
            fruits.Add("Blackcurrant");
            fruits.Add("Blueberry");
            fruits.Add("Cherry");
            fruits.Add("Coconut");
            fruits.Add("Cranberry");
            fruits.Add("Date");
            fruits.Add("Fig");
            fruits.Add("Grape");
            fruits.Add("Guava");
            fruits.Add("Jack-fruit");
            fruits.Add("Kiwi fruit");
            fruits.Add("Lemon");
            fruits.Add("Lime");
            fruits.Add("Lychee");
            fruits.Add("Mango");
            fruits.Add("Melon");
            fruits.Add("Olive");
            fruits.Add("Orange");
            fruits.Add("Papaya");
            fruits.Add("Plum");
            fruits.Add("Pineapple");
            fruits.Add("Pomegranate");

            // Local Actions
            Func<string> divider = () => { return new String('-', 70); };
            Action<string> printTitleAction = (title) => 
            {
                Console.WriteLine(divider());
                Console.WriteLine(title);
                Console.WriteLine(divider());
            };
            Action<string> printItemAction = (fruit) => {Console.WriteLine($"Fruit Name: {fruit}, Thread Id= {Thread.CurrentThread.ManagedThreadId}"); };
            Action<double> printTimeAction = (time) => 
            {
                Console.WriteLine(divider());
                Console.WriteLine($"Execution time = {time} seconds");
                Console.WriteLine(divider());
            };

            // Normal ForEach
            printTitleAction("Printing list using foreach loop");
            var stopWatch = Stopwatch.StartNew();
            foreach (string fruit in fruits) { printItemAction(fruit); }
            printTimeAction(stopWatch.Elapsed.TotalSeconds);

            // Parallel.ForEach
            printTitleAction("Printing list using Parallel.ForEach");
            stopWatch = Stopwatch.StartNew();
            Parallel.ForEach(fruits, fruit => { printItemAction(fruit); });
            printTimeAction(stopWatch.Elapsed.TotalSeconds);            
        }

        private static void ManipulateImageExample()
        {
            PrintUtility.PrintSubTitle("TIMED BITMAP MANIPULATION EXAMPLE");

            // Vars
            Stopwatch stopWatch = new Stopwatch();

            // Get Image Files
            String[] files = Directory.GetFiles(@"C:\Users\Public\Pictures\Sample Pictures", "*.jpg");

            // Create new directoty
            String newDir = @"C:\Users\Public\Pictures\Sample Pictures\Modified";            
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
            Action<string> manipulateImage = (file) =>
            {
                String filename = Path.GetFileName(file);
                var image = new Bitmap(file);
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                image.Save(Path.Combine(newDir, filename));
            };

            // Normal ForEach
            printTitleAction("Printing list using foreach loop");
            stopWatch = Stopwatch.StartNew();
            foreach(string file in files)
            {
                manipulateImage(file);
            }
            printTimeAction(stopWatch.Elapsed.TotalSeconds);

            // Parallel.ForEach
            printTitleAction("Printing list using Parallel.ForEach");
            stopWatch = Stopwatch.StartNew();
            Parallel.ForEach(files, (file) =>
            {
                manipulateImage(file);
            });
            printTimeAction(stopWatch.Elapsed.TotalSeconds);
        }

        private static void DoWork(int num)
        {
            Console.WriteLine($"Product Of {num}: {num * num}");
        }
    }
}
