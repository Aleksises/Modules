using BCL.Abstraction;
using System;

namespace BCL.Services
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
