using DAL.Abstractions;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories;
using NUnit.Framework;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Tests.IntegrationTests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        private readonly DbProviderFactory _factory = SqlClientFactory.Instance;

        private readonly string connectionString = @"Server=(local);Database=Northwind.1.3;Trusted_Connection = true";

        private static IOrderRepository _orderRepository;

        [SetUp]
        public void SetUp()
        {
            _orderRepository = new OrderRepository(_factory, connectionString);
        }

        [Test]
        public void GetOrders_WhenExecuted_ShouldReturnAllOrders()
        {
            var result = _orderRepository.GetAll();

            Assert.True(result.Any());
        }

        [Test]
        public void Get_WhenExecuted_ShouldReturnOrderWithProducts()
        {
            var result = _orderRepository.Get(10248);

            Assert.True(result != null);
            Assert.True(result.Products.Count > 0);
        }

        [Test]
        public void Add_WhenOrderProvided_ShouldReturnOrderWithOrderId()
        {
            var order = new Order();

            var updatedOrder = _orderRepository.Add(order);

            Assert.True(updatedOrder.OrderId > 0);
            Assert.True(updatedOrder.RequiredDate.HasValue);
        }

        [Test]
        public void ChangeOrder_InvalidOperationException()
        {
            var order = _orderRepository.Get(10248);

            Assert.Throws<InvalidOperationException>(() => order.OrderId = 10);
        }

        [Test]
        public void Delete_WhenExecuted_ShouldDeleteOrder()
        {
            var order = new Order();
            var updatedOrder = _orderRepository.Add(order);

            _orderRepository.Delete(updatedOrder.OrderId);

            var deletedOrder = _orderRepository.Get(updatedOrder.OrderId);

            Assert.IsNull(deletedOrder);
        }

        [Test]
        public void SetOrdered_WhenExecuted_ShouldUpdateOrderStatusOnInProgress()
        {
            var order = _orderRepository.Add(new Order());

            var updatedOrder = _orderRepository.SetOrderedDate(order.OrderId);

            Assert.True(updatedOrder.OrderDate != null && updatedOrder.GetStatus() == OrderStatus.InProgress);
        }

        [Test]
        public void SetDone_WhenExeuted_ShouldUpdateOrderStatusOnShipped()
        {
            var order = _orderRepository.Add(new Order());

            var updatedOrder = _orderRepository.SetOrderedDate(order.OrderId);
            updatedOrder = _orderRepository.SetDone(updatedOrder.OrderId);

            Assert.True(updatedOrder.ShippedDate != null && updatedOrder.GetStatus() == OrderStatus.Shipped);
        }

        [Test]
        public void GetCustomerOrderDetails_WhenExecuted_ShouldReturnCustomerOrderDetails()
        {
            var result = _orderRepository.GetCustomerOrderDetails(10248);

            Assert.True(result.Any());
        }

        [Test]
        public void GetCustomerOrderHistory_WhenExecuted_ShouldReturnCustomerOrderHistory()
        {
            var result = _orderRepository.GetCustomerOrderHistory("CHOPS");

            Assert.True(result.Any());
        }
    }
}
