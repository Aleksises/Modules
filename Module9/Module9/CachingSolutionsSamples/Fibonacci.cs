using Cache.Infrastructure.Cache;
using System;

namespace CachingSolutionsSamples
{
    public class Fibonacci
    {
        private readonly ICache<int> _cache;

        public Fibonacci(ICache<int> cache)
        {
            _cache = cache;
        }

        public int ComputeFibonacci(int index)
        {
            if (index <= 0)
            {
                throw new ArgumentException($"{nameof(index)} must be positive number");
            }

            if (index == 1 || index == 2)
            {
                return 1;
            }

            var fromCache = _cache.Get(index.ToString());
            if (fromCache != default)
            {
                return fromCache;
            }

            var result = ComputeFibonacci(index - 1) + ComputeFibonacci(index - 2);
            _cache.Set(index.ToString(), result, DateTimeOffset.Now.AddMilliseconds(500));
            
            return result;
        }
    }
}
