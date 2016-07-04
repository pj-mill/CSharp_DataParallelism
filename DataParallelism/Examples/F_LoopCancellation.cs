using DataParallelism.Utilities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataParallelism.Examples
{
    public class F_LoopCancellation
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("CANCELLING LOOPS");

            int[] nums = Enumerable.Range(0, 1000000).ToArray();
            CancellationTokenSource cts = new CancellationTokenSource();

            // Use ParallelOptions instance to store the CancellationToken
            ParallelOptions options = new ParallelOptions();
            options.CancellationToken = cts.Token;
            options.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            Console.WriteLine("Press any key to start. Press 'c' to cancel.");
            Console.ReadKey();

            // Run a task so that we can cancel from another thread.
            Task.Factory.StartNew(() =>
            {
                if(Console.ReadKey().KeyChar == 'c')
                {
                    cts.Cancel();
                    Console.WriteLine("Process cancelled");
                }
            });

            try
            {
                Parallel.ForEach(nums, options, (num) =>
                {
                    double sqrt = Math.Sqrt(num);
                    Console.WriteLine($"{sqrt} on {Thread.CurrentThread.ManagedThreadId}");
                    options.CancellationToken.ThrowIfCancellationRequested();
                });
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cts.Dispose();
            }            
        }        
    }
}
