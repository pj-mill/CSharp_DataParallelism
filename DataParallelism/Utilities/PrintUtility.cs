using System;

namespace DataParallelism.Utilities
{
    public class PrintUtility
    {
        #region PRINT METHODS
        public static void PrintTitle(string title)
        {
            Console.WriteLine("");
            Console.WriteLine("***************************************************");
            Console.WriteLine(title);
            Console.WriteLine("***************************************************");
        }

        public static void PrintSubTitle(string title)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("============================================================================================");
            Console.WriteLine(title);
            Console.WriteLine("============================================================================================");
        }
        #endregion
    }
}
