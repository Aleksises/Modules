using System.Runtime.Caching;

namespace Cache.Infrastructure.Cache
{
    public interface IMemoryCache<T> : ICache<T>
    {
        void Set(string key, T value, CacheItemPolicy cacheItemPolicy);
    }
}
