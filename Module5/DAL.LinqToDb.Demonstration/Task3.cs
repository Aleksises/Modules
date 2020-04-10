using DAL.LinqToDb.Demonstration.Infrastructure;
using DAL.LinqToDb.Entities;
using LinqToDB;
using LinqToDB.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.LinqToDb.Demonstration
{
    // To see results you can click at Open additional output in Test Explorer
    [TestFixture]
    public class Task3
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
        public void Add_new_Employee_with_Territories()
        {
            var newEmployee = new Employee
            {
                FirstName = "Aliaksei",
                LastName = "Patsekhin"
            };
            try
            {
                _connection.BeginTransaction();
                newEmployee.EmployeeId = Convert.ToInt32(_connection.InsertWithIdentity(newEmployee));
                _connection.Territories.Where(t => t.TerritoryDescription.Length <= 5)
                    .Insert(_connection.EmployeeTerritories, t => new EmployeeTerritory { EmployeeId = newEmployee.EmployeeId, TerritoryId = t.TerritoryId });
                _connection.CommitTransaction();
            }
            catch
            {
                _connection.RollbackTransaction();
            }
        }

        [Test]
        public void Move_Products_to_another_Category()
        {
            int updatedCount = _connection.Products.Update(p => p.CategoryId == 2, pr => new Product
            {
                CategoryId = 1
            });

            Console.WriteLine(updatedCount);
        }

        [Test]
        public void Add_list_of_Products_with_Suppliers_and_Categories()
        {
            var products = new List<Product>
            {
                new Product
                {
                    ProductName = "Pizza",
                    Category = new Category {CategoryName = "Italian food"},
                    Supplier = new Supplier {CompanyName = "Mama Mia"}
                },
                new Product
                {
                    ProductName = "Pasta",
                    Category = new Category {CategoryName = "Italian food"},
                    Supplier = new Supplier {CompanyName = "Mama Mia"}
                }
            };

            try
            {
                _connection.BeginTransaction();
                foreach (var product in products)
                {
                    var category = _connection.Categories.FirstOrDefault(c => c.CategoryName == product.Category.CategoryName);
                    product.CategoryId = category?.CategoryId ?? Convert.ToInt32(_connection.InsertWithIdentity(
                                             new Category
                                             {
                                                 CategoryName = product.Category.CategoryName
                                             }));
                    var supplier = _connection.Suppliers.FirstOrDefault(s => s.CompanyName == product.Supplier.CompanyName);
                    product.SupplierId = supplier?.SupplierId ?? Convert.ToInt32(_connection.InsertWithIdentity(
                                             new Supplier
                                             {
                                                 CompanyName = product.Supplier.CompanyName
                                             }));
                }

                _connection.BulkCopy(products);
                _connection.CommitTransaction();
            }
            catch
            {
                _connection.RollbackTransaction();
            }
        }

        [Test]
        public void Replace_Product_with_the_same_in_NotShippedOrders_one_query()
        {
            var updatedRows = _connection.OrderDetails.LoadWith(od => od.Order).LoadWith(od => od.Product)
                .Where(od => od.Order.ShippedDate == null).Update(
                    od => new OrderDetail
                    {
                        ProductId = _connection.Products.First(p => p.CategoryId == od.Product.CategoryId && p.ProductId > od.ProductId) != null
                            ? _connection.Products.First(p => p.CategoryId == od.Product.CategoryId && p.ProductId > od.ProductId).ProductId
                            : _connection.Products.First(p => p.CategoryId == od.Product.CategoryId).ProductId
                    });
            Console.WriteLine($"{updatedRows} rows updated");
        }
    }
}
