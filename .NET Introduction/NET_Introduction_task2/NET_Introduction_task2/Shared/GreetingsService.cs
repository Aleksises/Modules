using System;

namespace Shared
{
    public class GreetingsService
    {
        public string GetGreetingsString(string username)
        {
            return string.Format("{0}, Hello, {1}", DateTime.Now, username);
        }
    }
}
