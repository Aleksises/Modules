using System;
using WebDownloader.Interfaces;

namespace WebDownloader.Services
{
    public class Logger : ILogger
    {
        private readonly bool _isLogEnabled;

        public Logger(bool isLogEnabled)
        {
            _isLogEnabled = isLogEnabled;
        }

        public void Log(string message)
        {
            if (_isLogEnabled)
            {
                Console.WriteLine(message);
            }
        }
    }
}
