using Shared;
using System;

namespace NETCoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var service = new GreetingsService();
                var greetingsString = service.GetGreetingsString(args[0]);
                Console.WriteLine(greetingsString);
            }
            else
            {
                Console.WriteLine("You did not provide a username!");
            }
            Console.ReadKey();
        }
    }
}
