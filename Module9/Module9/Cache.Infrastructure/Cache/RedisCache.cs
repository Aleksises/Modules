using StackExchange.Redis;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace Cache.Infrastructure.Cache
{
    public class RedisCache<T> : ICache<T>
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(T));
        private readonly IDatabase _database;
        private readonly string _prefix;

        public RedisCache(string hostName, string prefix)
        {
            _prefix = prefix;
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                ConnectTimeout = 20000,
                EndPoints = { hostName }
            };
            _redisConnection = ConnectionMultiplexer.Connect(options);
            _database = _redisConnection.GetDatabase();
        }

        public T Get(string key)
        {
            byte[] value = _database.StringGet(_prefix + key);

            return value == null ? default : (T)_serializer.ReadObject(new MemoryStream(value));
        }

        public void Set(string key, T value, DateTimeOffset expirationDate)
        {
            var redisKey = _prefix + key;

            if (value == null)
            {
                _database.StringSet(redisKey, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                _serializer.WriteObject(stream, value);
                _database.StringSet(redisKey, stream.ToArray(), expirationDate - DateTimeOffset.Now);
            }
        }
    }
}
