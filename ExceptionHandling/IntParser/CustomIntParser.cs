using System;

namespace IntParser
{
    public static class CustomIntParser
    {
        public static bool TryParse(string source, out int result)
        {
			try
			{
				result = Convert.ToInt32(source);

				return true;
			}
			catch (Exception ex)
			{
				var t = ex.GetType();
				result = default(int);
				Console.WriteLine("The provided string don't contains only numeric characters!");

				return false;
			}
        }
    }
}
