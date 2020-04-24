using System.Collections.Generic;

namespace Cache.Infrastructure.Managers
{
    public interface IEntitiesManager<T> where T : class
    {
        IEnumerable<T> GetEntities();
    }
}
