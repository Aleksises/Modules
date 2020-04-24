using Cache.Infrastructure.Cache;
using NUnit.Framework;
using System;
using System.Threading;

namespace CachingSolutionsSamples
{
    [TestFixture]
    public class Task1Tests
    {
        private readonly string _fibonacciPrefix = "_fibonacci";

        [Test]
        public void FibonacciMemoryCache()
        {
            var fibonacci = new Fibonacci(new MemoryCache<int>(_fibonacciPrefix));

            for (var i = 1; i < 30; i++)
            {
                Console.WriteLine(fibonacci.ComputeFibonacci(i));
                Thread.Sleep(100);
            }
        }

        [Test]
        public void FibonacciRedisCache()
        {
            var fibonacci = new Fibonacci(new RedisCache<int>("localhost", _fibonacciPrefix));

            for (var i = 1; i < 30; i++)
            {
                Console.WriteLine(fibonacci.ComputeFibonacci(i));
                Thread.Sleep(100);
            }
        }
    }
}
