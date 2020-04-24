using Cache.Infrastructure.Cache;
using NorthwindLibrary;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;

namespace Cache.Infrastructure.Managers
{
    public class MemoryEntitiesManager<T> : IEntitiesManager<T> where T : class
    {
        private readonly IMemoryCache<IEnumerable<T>> _cache;
        private readonly string _monitorCommand;

        public MemoryEntitiesManager(IMemoryCache<IEnumerable<T>> cache, string monitorCommand)
        {
            _cache = cache;
            _monitorCommand = monitorCommand;
        }

        public IEnumerable<T> GetEntities()
        {
            var user = Thread.CurrentPrincipal.Identity.Name;
            var entities = _cache.Get(user);

            if (entities == null)
            {
                string connectionString;
                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    entities = dbContext.Set<T>().ToList();
                    connectionString = dbContext.Database.Connection.ConnectionString;
                }

                SqlDependency.Start(connectionString);
                _cache.Set(user, entities, GetCachePolicy(_monitorCommand, connectionString));
            }

            return entities;
        }

        private CacheItemPolicy GetCachePolicy(string monitorCommand, string connectionString)
        {
            return new CacheItemPolicy
            {
                ChangeMonitors = { GetMonitor(monitorCommand, connectionString) }
            };
        }

        private ChangeMonitor GetMonitor(string monitorCommand, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(monitorCommand, connection);
                var monitor = new SqlChangeMonitor(new SqlDependency(command));
                command.ExecuteNonQuery();
                return monitor;
            }
        }
    }
}
