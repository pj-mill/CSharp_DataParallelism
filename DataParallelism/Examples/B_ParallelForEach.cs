using DataParallelism.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataParallelism.Examples
{
    public class B_ParallelForEach
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("PARALLEL FOREACH");
            SimpleExample();
        }

        private static void SimpleExample()
        {
            PrintUtility.PrintSubTitle("SIMPLE EXAMPLE");

            int[] nums = Enumerable.Range(0, 10).ToArray();
            Parallel.ForEach(nums, (num) => { DoWork(num); });
        }

        private static void DoWork(int num)
        {
            Console.WriteLine($"Product Of {num}: {num * num}");
        }
    }
}
