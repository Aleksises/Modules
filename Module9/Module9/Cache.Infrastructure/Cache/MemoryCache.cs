using System;
using System.Runtime.Caching;

namespace Cache.Infrastructure.Cache
{
    public class MemoryCache<T> : IMemoryCache<T>
    {
        private readonly ObjectCache _cache = MemoryCache.Default;
        private readonly string _prefix;

        public MemoryCache(string prefix)
        {
            _prefix = prefix;
        }

        public T Get(string key)
        {
            var fromCache = _cache.Get(_prefix + key);

            return fromCache == null ? default : (T)fromCache;
        }

        public void Set(string key, T value, DateTimeOffset expirationDate)
        {
            _cache.Set(_prefix + key, value, expirationDate);
        }

        public void Set(string key, T value, CacheItemPolicy cacheItemPolicy)
        {
            _cache.Set(_prefix + key, value, cacheItemPolicy);
        }
    }
}
