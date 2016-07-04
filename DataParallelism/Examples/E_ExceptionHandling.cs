using DataParallelism.Utilities;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DataParallelism.Examples
{
    public class E_ExceptionHandling
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("EXCEPTION HANDLING");
            // Create some random data to process in parallel.
            // There is a good probability this data will cause some exceptions to be thrown.
            byte[] nums = new byte[10000];
            Random r = new Random();
            r.NextBytes(nums);      // Fill array with numbers

            try
            {
                ProcessDataInParallel(nums);
            }
            catch(AggregateException ex)
            {
                foreach(var e in ex.InnerExceptions)
                {
                    if(e is ArgumentException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }

        private static void ProcessDataInParallel(byte[] nums)
        {
            // Use ConcurrentQueue to enable safe enqueueing from multiple threads.
            var exceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(nums, number =>
            {
                try
                {
                    if(number < 0x3)
                    {
                        throw new ArgumentException($"Number is {number}; Cannot be lower than 3");
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Enqueue(ex);
                }
            });

            if(exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
