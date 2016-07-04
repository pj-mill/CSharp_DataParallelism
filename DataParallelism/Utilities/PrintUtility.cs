using System;

namespace DataParallelism.Utilities
{
    public class PrintUtility
    {
        #region PRINT METHODS
        public static void PrintTitle(string title)
        {
            string divider = new String('*', 70);
            Console.WriteLine("");
            Console.WriteLine(divider);
            Console.WriteLine(title);
            Console.WriteLine(divider);
        }

        public static void PrintSubTitle(string title)
        {
            string divider = new String('=', 70);
            Console.WriteLine("");
            Console.WriteLine(divider);
            Console.WriteLine(title);
            Console.WriteLine(divider);
        }
        #endregion
    }
}
