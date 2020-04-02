// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Task")]
		[Title("Task 001")]
		[Description("Displays all customers with sum of orders greater than X")]
		public void Linq001()
		{
			var x = 50000;
			var customers = dataSource.Customers
				.Where(c => c.Orders.Sum(o => o.Total) > x)
				.Select(c => new
				{
					CustomerId = c.CustomerID,
					TotalSum = c.Orders.Sum(o => o.Total)
				});

			ObjectDumper.Write($"Greater than {x}");
			foreach (var customer in customers)
			{
				ObjectDumper.Write($"CustomerId = {customer.CustomerId} TotalSum = {customer.TotalSum}\n");
			}

			x = 500;
			ObjectDumper.Write($"Greater than {x}");
			foreach (var customer in customers)
			{
				ObjectDumper.Write($"CustomerId = {customer.CustomerId} TotalSum = {customer.TotalSum}\n");
			}
		}

		[Category("Task")]
		[Title("Task 002")]
		[Description("Displays all customers with theirs suppliers from the same country and city")]
		public void Linq002()
		{
			var customersWithSuppliers = dataSource.Customers
				.Select(c => new
				{
					Customer = c,
					Suppliers = dataSource.Suppliers.Where(s => s.City == c.City && s.Country == c.Country)
				});

			ObjectDumper.Write("Without grouping\n");
			foreach (var customer in customersWithSuppliers)
			{
				ObjectDumper.Write($"CustomerId: {customer.Customer.CustomerID} " +
					$"Suppliers: {string.Join(", ", customer.Suppliers.Select(s => s.SupplierName))}");
			}

			var customers = dataSource.Customers.GroupJoin(dataSource.Suppliers,
				c => new { c.City, c.Country },
				s => new { s.City, s.Country },
				(c, s) => new { Customer = c, Suppliers = s });

			ObjectDumper.Write("With grouping\n");
			foreach (var customer in customers)
			{
				ObjectDumper.Write($"CustomerId: {customer.Customer.CustomerID} " +
					$"Suppliers: {string.Join(", ", customer.Suppliers.Select(s => s.SupplierName))}");
			}
		}

		[Category("Task")]
		[Title("Task 003")]
		[Description("Displays all clients who has orders with total greater than X")]
		public void Linq003()
		{
			var x = 1000;
			var customers = dataSource.Customers.Where(c => c.Orders.Any(o => o.Total > x));

			ObjectDumper.Write($"Greater than {x}");
			foreach (var customer in customers)
			{
				ObjectDumper.Write(customer);
			}
		}

		[Category("Task")]
		[Title("Task 004")]
		[Description("Displays all clients with their first orders month and year")]
		public void Linq004()
		{
			var customers = dataSource.Customers
				.Where(c => c.Orders.Any())
				.Select(c => new
				{
					CustomerId = c.CustomerID,
					StartDate = c.Orders.OrderBy(o => o.OrderDate).First().OrderDate
				});

			foreach (var customer in customers)
			{
				ObjectDumper.Write($"CustomerId = {customer.CustomerId} Year = {customer.StartDate.Year} Month = {customer.StartDate.Month}");
			}
		}

		[Category("Task")]
		[Title("Task 005")]
		[Description("Displays all clients with their first orders month and year ordered by"
			+ "year, month, sum of orders total, clientName")]
		public void Linq005()
		{
			var customers = dataSource.Customers
				.Where(c => c.Orders.Any())
				.Select(c => new
				{
					CustomerId = c.CustomerID,
					StartDate = c.Orders.OrderBy(o => o.OrderDate).First().OrderDate,
					TotalSum = c.Orders.Sum(o => o.Total)
				})
				.OrderByDescending(c => c.StartDate.Year)
				.ThenByDescending(c => c.StartDate.Month)
				.ThenByDescending(c => c.TotalSum)
				.ThenByDescending(c => c.CustomerId);

			foreach (var customer in customers)
			{
				ObjectDumper.Write($"CustomerId = {customer.CustomerId} TotalSum = {customer.TotalSum}"
					+ $"Year = {customer.StartDate.Year} Month = {customer.StartDate.Month}");
			}
		}

		[Category("Task")]
		[Title("Task 006")]
		[Description("Displays all customers with not numeric postal code or without region or whithout operator's code")]
		public void Linq006()
		{
			var customers = dataSource.Customers.Where(
				c => c.PostalCode != null && c.PostalCode.Any(ch => ch < '0' || ch > '9')
					|| string.IsNullOrWhiteSpace(c.Region)
					|| c.Phone.FirstOrDefault() != '(');

			foreach (var customer in customers)
			{
				ObjectDumper.Write(customer);
			}
		}

		[Category("Task")]
		[Title("Task 007")]
		[Description("Displays grouped products by categories then by units in stock > 0  then order by unitPrice")]
		public void Linq007()
		{
			var groups = dataSource.Products
				.GroupBy(p => p.Category)
				.Select(g => new
				{
					Category = g.Key,
					ProductsByStock = g
						.GroupBy(p => p.UnitsInStock > 0)
						.Select(a => new
						{
							HasInStock = a.Key,
							Products = a.OrderBy(prod => prod.UnitPrice)
						})
				});

			foreach (var productsByCategory in groups)
			{
				ObjectDumper.Write($"Category: {productsByCategory.Category}\n");
				foreach (var productsByStock in productsByCategory.ProductsByStock)
				{
					ObjectDumper.Write($"\tHas in stock: {productsByStock.HasInStock}");
					foreach (var product in productsByStock.Products)
					{
						ObjectDumper.Write($"\t\tProduct: {product.ProductName} Price: {product.UnitPrice}");
					}
				}
			}
		}

		[Category("Task")]
		[Title("Task 008")]
		[Description("Displays grouped products by price: Cheap, Average, Expensive")]
		public void Linq008()
		{
			var cheapPrice = 10m;
			var expensivePrice = 60m;

			var productGroups = dataSource.Products
				.GroupBy(p => p.UnitPrice < cheapPrice ? "Cheap"
					: p.UnitPrice < expensivePrice ? "Average" : "Expensive");

			foreach (var productGroup in productGroups)
			{
				ObjectDumper.Write($"{productGroup.Key}");
				foreach (var product in productGroup)
				{
					ObjectDumper.Write($"\tProduct = {product.ProductName} Price = {product.UnitPrice}");
				}
			}
		}

		[Category("Task")]
		[Title("Task 009")]
		[Description("Displays average order sum and average client's intensity for every city")]
		public void Linq009()
		{
			var customers = dataSource.Customers
				.GroupBy(c => c.City)
				.Select(c => new
				{
					City = c.Key,
					Intensity = c.Average(p => p.Orders.Length),
					AverageIncome = c.Average(p => p.Orders.Sum(o => o.Total))
				});

			foreach (var customer in customers)
			{
				ObjectDumper.Write($"City: {customer.City}");
				ObjectDumper.Write($"\tIntensity: {customer.Intensity}");
				ObjectDumper.Write($"\tAverage Income: {customer.AverageIncome}");
			}
		}

		[Category("Task")]
		[Title("Task 010")]
		[Description("Displays clients activity statistic for month (without year), year and for year and month")]
		public void Linq010()
		{
			var statistic = dataSource.Customers
				.Select(c => new
				{
					c.CustomerID,
					MonthsStatistic = c.Orders.GroupBy(o => o.OrderDate.Month)
										.Select(g => new { Month = g.Key, OrdersCount = g.Count() }),
					YearsStatistic = c.Orders.GroupBy(o => o.OrderDate.Year)
										.Select(g => new { Year = g.Key, OrdersCount = g.Count() }),
					YearMonthStatistic = c.Orders
										.GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
										.Select(g => new { g.Key.Year, g.Key.Month, OrdersCount = g.Count() })
				});

			foreach (var record in statistic)
			{
				ObjectDumper.Write($"CustomerId: {record.CustomerID}");
				ObjectDumper.Write("\tMonths statistic:\n");
				foreach (var ms in record.MonthsStatistic)
				{
					ObjectDumper.Write($"\t\tMonth: {ms.Month} Orders count: {ms.OrdersCount}");
				}
				ObjectDumper.Write("\tYears statistic:\n");
				foreach (var ys in record.YearsStatistic)
				{
					ObjectDumper.Write($"\t\tYear: {ys.Year} Orders count: {ys.OrdersCount}");
				}
				ObjectDumper.Write("\tYear and month statistic:\n");
				foreach (var ym in record.YearMonthStatistic)
				{
					ObjectDumper.Write($"\t\tYear: {ym.Year} Month: {ym.Month} Orders count: {ym.OrdersCount}");
				}
			}
		}
	}
}
