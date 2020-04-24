using Cache.Infrastructure.Cache;
using Cache.Infrastructure.Managers;
using NorthwindLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
    [TestFixture]
    public class Task2Tests
    {
        private readonly string _categoriesPrefix = "_cache_Categories";
        private readonly string _customersPrefix = "_cache_Customers";
        private readonly string _suppliersPrefix = "_cache_Suppliers";

        [Test]
        public void CategoriesMemoryCache()
        {
            var entitiesManager = new EntitiesManager<Category>(new MemoryCache<IEnumerable<Category>>(_categoriesPrefix));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [Test]
        public void CategoriesRedisCache()
        {
            var entitiesManager = new EntitiesManager<Category>(new RedisCache<IEnumerable<Category>>("localhost", _categoriesPrefix));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [Test]
        public void CustomersMemoryCache()
        {
            var entitiesManager = new EntitiesManager<Customer>(new MemoryCache<IEnumerable<Customer>>(_customersPrefix));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [Test]
        public void CustomersRedisCache()
        {
            var entitiesManager = new EntitiesManager<Customer>(new RedisCache<IEnumerable<Customer>>("localhost", _customersPrefix));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [Test]
        public void SuppliersMemoryCache()
        {
            var entitiesManager = new EntitiesManager<Supplier>(new MemoryCache<IEnumerable<Supplier>>(_suppliersPrefix));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [Test]
        public void SuppliersRedisCache()
        {
            var entitiesManager = new EntitiesManager<Supplier>(new RedisCache<IEnumerable<Supplier>>("localhost", _suppliersPrefix));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [Test]
        public void SqlMonitorsTest()
        {
            var entitiesManager = new MemoryEntitiesManager<Supplier>(new MemoryCache<IEnumerable<Supplier>>(_suppliersPrefix),
                "select [SupplierID],[CompanyName],[ContactName],[ContactTitle],[Address],[City],[Region],[PostalCode],[Country],[Phone],[Fax] from [dbo].[Suppliers]");
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
            }
        }
    }
}
