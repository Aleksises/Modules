using Cache.Infrastructure.Cache;
using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Cache.Infrastructure.Managers
{
    public class EntitiesManager<T> : IEntitiesManager<T> where T : class
    {
        private readonly ICache<IEnumerable<T>> _cache;

        public EntitiesManager(ICache<IEnumerable<T>> cache)
        {
            _cache = cache;
        }

        public IEnumerable<T> GetEntities()
        {
            var user = Thread.CurrentPrincipal.Identity.Name;
            var entities = _cache.Get(user);

            if (entities == null)
            {
                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    entities = dbContext.Set<T>().ToList();
                }

                _cache.Set(user, entities, DateTimeOffset.Now.AddDays(1));
            }

            return entities;
        }
    }
}
