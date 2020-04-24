using System;

namespace Cache.Infrastructure.Cache
{
    public interface ICache<T>
    {
        T Get(string key);

        void Set(string key, T value, DateTimeOffset expirationDate);
    }
}
