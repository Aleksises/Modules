using System;

namespace ExceptionHandling
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter a string:");
            var input = Console.ReadLine();
            var strings = input.Split(" ");

            foreach (var str in strings)
            {
                try
                {
                    Console.WriteLine(str[0]);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("You entered an empty string!");
                }
            }
        }
    }
}
