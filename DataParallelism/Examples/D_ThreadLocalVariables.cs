using DataParallelism.Utilities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataParallelism.Examples
{
    /// <summary>
    /// This example shows how to use thread-local variables to store and retrieve state in each separate task that is created by a Parallel loop.
    /// By using thread-local data, you can avoid the overhead of synchronizing a large number of accesses to shared state. 
    /// Instead of writing to a shared resource on each iteration, you compute and store the value until all iterations for the task are complete. 
    /// You can then write the final result once to the shared resource, or pass it to another method.
    /// </summary>
    public class D_ThreadLocalVariables
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("THREAD LOCAL VARIABLES");
            int[] nums = Enumerable.Range(0, 1000000).ToArray();
            ThreadLocalForLoop(nums);
            ThreadLocalForEachLoop(nums);
        }

        private static void ThreadLocalForLoop(int[] nums)
        {
            PrintUtility.PrintSubTitle("Parallel.For Example");

            // Vars            
            long total = 0;

            // Use type parameter to make subtotal a long, not an int
            Parallel.For<long>(
                0,                          // Lower bound index
                nums.Length,                // Upper bound index (-1)
                () => 0,                    // method to initialize the local variable
                (idx, loop, subtotal) =>    // method invoked by the loop on each iteration
                {
                    subtotal += nums[idx];
                    return subtotal;        // value to be passed to next iteration
                },
                    // Method to be executed when each partition has completed.
                    // finalResult is the final value of subtotal for a particular partition.
                    (x) => Interlocked.Add(ref total, x)
                );

            Console.WriteLine("The total from Parallel.For is {0:N0}", total);
        }

        private static void ThreadLocalForEachLoop(int[] nums)
        {
            PrintUtility.PrintSubTitle("Parallel.ForEach Example");

            // Vars
            long total = 0;

            // First type parameter is the type of the source elements
            // Second type parameter is the type of the thread-local variable (partition subtotal)
            Parallel.ForEach<int, long>(
                nums,                       // source collection
                () => 0,                    // method to initialize the local variable
                (idx, loop, subtotal) =>    // method invoked by the loop on each iteration
                {
                    subtotal += nums[idx];
                    return subtotal;        // value to be passed to next iteration
                },
                    // Method to be executed when each partition has completed.
                    // finalResult is the final value of subtotal for a particular partition.
                    (x) => Interlocked.Add(ref total, x)
                );

            Console.WriteLine("The total from Parallel.ForEach is {0:N0}", total);
        }
    }
}
