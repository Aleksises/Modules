using System;

namespace Shared
{
    public static class GreetingsService
    {
        public static string GetGreetingsString(string username)
        {
            return string.Format("{0} Hello, {1}", DateTime.Now, username);
        }
    }
}
