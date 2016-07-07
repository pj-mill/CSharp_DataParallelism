using DataParallelism.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataParallelism.Examples
{
    /// <summary>
    /// When a Parallel.For loop has a small body, it might perform more slowly than the equivalent sequential loop, 
    /// such as the for loop in C# and the For loop in Visual Basic. 
    /// Slower performance is caused by the overhead involved in partitioning the data and the cost of invoking a delegate on each loop iteration. 
    /// To address such scenarios, the Partitioner class provides the Partitioner.
    /// Create method, which enables you to provide a sequential loop for the delegate body, so that the delegate is invoked only once per partition, 
    /// instead of once per iteration.
    /// </summary>
    public class G_PerformanceEnhancement
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("PERFORMANCE ENHANCEMENT");

            // Vars
            Stopwatch stopWatch = new Stopwatch();
            int[] nums = Enumerable.Range(0, 1000000).ToArray();
            List<double> results = new List<double>();
            string divider = new String('-', 70);

            // Local Delegates
            Action<string> printTitleAction = (title) =>
            {
                Console.WriteLine(divider);
                Console.WriteLine(title);
                Console.WriteLine(divider);
            };
            Action<double> printTimeAction = (time) =>
            {
                Console.WriteLine(divider);
                Console.WriteLine($"Execution time = {time} seconds");
                Console.WriteLine(divider);
            };


            // Normal ForEach
            printTitleAction("STANDARD FOREACH LOOP");
            stopWatch = Stopwatch.StartNew();
            foreach(int num in nums)
            {
                results.Add(num * Math.PI);
            }
            printTimeAction(stopWatch.Elapsed.TotalSeconds);


            // Parallel ForEach
            printTitleAction("PARALLEL FOREACH WITHOUT PARTITIONER");
            results.Clear();
            stopWatch = Stopwatch.StartNew();
            Parallel.ForEach(nums, (num) =>
            {
                results.Add(num * Math.PI);
            });
            printTimeAction(stopWatch.Elapsed.TotalSeconds);


            // Parallel ForEach with partitionaer
            printTitleAction("PARALLEL FOREACH WITH PARTITIONER");
            results.Clear();
            stopWatch = Stopwatch.StartNew();

            // Partition the entire source array.
            var rangePartitioner = Partitioner.Create(0, nums.Length);
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                for(int i = range.Item1; i < range.Item2; i++)
                {
                    results.Add(nums[i] * Math.PI);
                }
            });
            printTimeAction(stopWatch.Elapsed.TotalSeconds);
        }
    }
}
