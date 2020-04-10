using DAL.LinqToDb.Demonstration.Infrastructure;
using LinqToDB;
using LinqToDB.Data;
using NUnit.Framework;
using System;
using System.Linq;

namespace DAL.LinqToDb.Demonstration
{
    // To see results you can click at Open additional output in Test Explorer
    [TestFixture]
    public class Task2
    {
        private NorthwindConnection _connection;

        [SetUp]
        public void SetUp()
        {
            DataConnection.DefaultSettings = new NorthwindSettings();
            _connection = new NorthwindConnection("Northwind");
        }

        [TearDown]
        public void CleanUp()
        {
            _connection.Dispose();
        }

        [Test]
        public void List_of_products_with_category_and_supplier()
        {
            foreach (var product in _connection.Products.LoadWith(p => p.Category).LoadWith(p => p.Supplier).ToList())
            {
                Console.WriteLine($"Product name: {product.ProductName}; Category: {product.Category?.CategoryName}; Supplier: {product.Supplier?.ContactName}");
            }
        }

        [Test]
        public void List_of_employees_with_region()
        {
            var query = from e in _connection.Employees
                        join et in _connection.EmployeeTerritories on e.EmployeeId equals et.EmployeeId into el
                        from w in el.DefaultIfEmpty()
                        join t in _connection.Territories on w.TerritoryId equals t.TerritoryId into zl
                        from z in zl.DefaultIfEmpty()
                        join r in _connection.Regions on z.RegionId equals r.RegionId into kl
                        from k in kl.DefaultIfEmpty()
                        select new { e.FirstName, e.LastName, Region = k };
            query = query.Distinct();

            foreach (var record in query.ToList())
            {
                Console.WriteLine($"Employee: {record.FirstName} {record.LastName}; Region: {record.Region?.RegionDescription}");
            }
        }

        [Test]
        public void Statistic_Count_of_employees_by_regions()
        {
            var query = from r in _connection.Regions
                        join t in _connection.Territories on r.RegionId equals t.RegionId into kl
                        from k in kl.DefaultIfEmpty()
                        join et in _connection.EmployeeTerritories on k.TerritoryId equals et.TerritoryId into zl
                        from z in zl.DefaultIfEmpty()
                        join e in _connection.Employees on z.EmployeeId equals e.EmployeeId into dl
                        from d in dl.DefaultIfEmpty()
                        select new { Region = r, d.EmployeeId };
            var result = from row in query.Distinct()
                         group row by row.Region into ger
                         select new { RegionDescription = ger.Key.RegionDescription, EmployeesCount = ger.Count(e => e.EmployeeId != 0) };

            foreach (var record in result.ToList())
            {
                Console.WriteLine($"Region: {record.RegionDescription}; Employees count: {record.EmployeesCount}");
            }
        }

        [Test]
        public void Employees_with_Shippers_according_to_Orders()
        {
            var query = (from e in _connection.Employees
                         join o in _connection.Orders on e.EmployeeId equals o.EmployeeId into el
                         from w in el.DefaultIfEmpty()
                         join s in _connection.Shippers on w.ShipperId equals s.ShipperId into zl
                         from z in zl.DefaultIfEmpty()
                         select new { e.EmployeeId, e.FirstName, e.LastName, z.CompanyName }).Distinct().OrderBy(t => t.EmployeeId);

            foreach (var record in query.ToList())
            {
                Console.WriteLine($"Employee: {record.FirstName} {record.LastName} Shipper: {record.CompanyName}");
            }
        }
    }
}
