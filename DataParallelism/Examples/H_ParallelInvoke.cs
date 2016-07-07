using DataParallelism.Utilities;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace DataParallelism.Examples
{
    public class H_ParallelInvoke
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("INVOKE EXAMPLE");

            BlockingCollection<int> bc = new BlockingCollection<int>();

            int numberOfItems = 1000;
            int outerSum = 0;

            // Generate some numbers
            for (int i = 0; i < numberOfItems; ++i)
            {
                bc.Add(i);
            }
            bc.CompleteAdding();

            // Delegate for adding up all numbers
            Action action = () =>
            {
                int item;
                int innerSum = 0;

                while (bc.TryTake(out item))
                {
                    innerSum += item;
                }

                Interlocked.Add(ref outerSum, innerSum);
            };

            // Launch three parallel actions to consume the BlockingCollection
            Parallel.Invoke(action, action, action);

            // Output
            Console.WriteLine($"Sum[0..{numberOfItems}) = {outerSum}, should be {(numberOfItems * (numberOfItems - 1)) / 2}");

            // Cleanup
            bc.Dispose();
        }
    }
}
