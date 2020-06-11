using System;

namespace NETCoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine("Hello, {0}",args[0]);
            }
            else
            {
                Console.WriteLine("You did not provide a username!");
            }
            Console.ReadKey();
        }
    }
}
