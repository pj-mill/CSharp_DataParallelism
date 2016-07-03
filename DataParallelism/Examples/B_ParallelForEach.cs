using DataParallelism.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParallelism.Examples
{
    public class B_ParallelForEach
    {
        public static void Run()
        {
            PrintUtility.PrintTitle("PARALLEL FOREACH");

        }

        private static void SimpleExample()
        {
            PrintUtility.PrintSubTitle("SIMPLE EXAMPLE");
            
        }

        private static void DoWork(int num)
        {
            Console.WriteLine($"Product Of {num}: {num * num}");
        }
    }
}
