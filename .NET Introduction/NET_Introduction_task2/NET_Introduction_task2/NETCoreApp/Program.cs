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
                var greetings = GreetingsService.GetGreetingsString(args[0]);
                Console.WriteLine(greetings);
            }
            else
            {
                Console.WriteLine("You did not provide a username!");
            }
            Console.ReadKey();
        }
    }
}
