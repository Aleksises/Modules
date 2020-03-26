using DAL.Abstractions;
using DAL.Mappers;
using DAL.Models;
using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly DbConnection _connection;

        public OrderRepository(DbProviderFactory providerFactory, string connectionString)
        {
            _providerFactory = providerFactory ?? throw new ArgumentNullException(nameof(providerFactory));

            _connection = providerFactory.CreateConnection();
            _connection.ConnectionString = connectionString;
        }

        public Order Add(Order order)
        {
            var command = StartExecution();

            command.CommandText = "INSERT INTO [Orders] (requireddate) values (getdate());" +
                                  "SELECT @@IDENTITY";
            command.ExecuteNonQuery();
            var orderId = Convert.ToInt32(command.ExecuteScalar());

            EndExecution(command);

            return Get(orderId);
        }

        public Order Get(int id)
        {
            const int orderTable = 0;
            const int productTable = 1;

            var command = StartExecution();

            command.CommandText =
                "SELECT " +
                " [OrderID]" +
                ",[OrderDate]" +
                ",[RequiredDate]" +
                ",[ShippedDate]" +
                " FROM [Orders] AS MyOrder" +
                " WHERE MyOrder.[OrderID] = @id;" +

                "SELECT" +
                " Product.ProductID," +
                " Product.ProductName" +
                " FROM [Products] AS Product" +
                " JOIN [Order Details] AS OrderDetails" +
                " ON Product.[ProductID] = OrderDetails.[ProductID]" +
                " JOIN [Orders] AS MyOrder" +
                " ON MyOrder.[OrderID] = @id" +
                " GROUP BY Product.[ProductID]," +
                " Product.[ProductName];";

            command.CommandType = CommandType.Text;

            var paramId = command.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;

            command.Parameters.Add(paramId);

            var dataAdapter = _providerFactory.CreateDataAdapter();
            var dataSet = new DataSet();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[orderTable].Rows.Count == 0)
            {
                return null;
            }

            var order = SqlMapper.Map<Order>(dataSet.Tables[orderTable].Rows[0]);

            if (dataSet.Tables[productTable].Rows.Count > 0)
            {
                order.Products = new List<Product>();

                foreach (DataRow row in dataSet.Tables[productTable].Rows)
                {
                    order.Products.Add(SqlMapper.Map<Product>(row));
                }
            }

            EndExecution(command);

            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = new List<Order>();

            var command = StartExecution();

            command.CommandText =
                "SELECT " +
                " [OrderID]" +
                ",[OrderDate]" +
                ",[RequiredDate]" +
                ",[ShippedDate]" +
                " FROM [Orders]";

            command.CommandType = CommandType.Text;

            var dataAdapter = _providerFactory.CreateDataAdapter();

            dataAdapter.SelectCommand = command;
            var table = new DataTable();

            dataAdapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                orders.Add(SqlMapper.Map<Order>(row));
            }

            EndExecution(command);

            return orders;
        }

        public void Delete(int id)
        {
            var order = Get(id);

            if (order.GetStatus() == OrderStatus.Shipped)
            {
                return;
            }

            var command = StartExecution();

            command.CommandText = "DELETE [Orders] WHERE [OrderID] = @id";

            var param = command.CreateParameter();
            param.ParameterName = "@id";
            param.Value = id;

            command.Parameters.Add(param);
            command.ExecuteNonQuery();

            EndExecution(command);
        }

        public Order SetOrderedDate(int orderId) =>
            UpdateStatus("UPDATE [Orders] SET [OrderDate] = GETDATE() WHERE [OrderId] = @id", orderId);

        public Order SetDone(int orderId) =>
            UpdateStatus("UPDATE [Orders] SET [ShippedDate] = GETDATE() WHERE [OrderId] = @id ", orderId);

        public IEnumerable<CustomerOrderDetails> GetCustomerOrderDetails(int orderId)
        {
            var customerOrderDetails = new List<CustomerOrderDetails>();
            var command = StartExecution();

            command.CommandText = "[CustOrdersDetail]";
            command.CommandType = CommandType.StoredProcedure;

            var orderIdParam = command.CreateParameter();
            orderIdParam.ParameterName = "@OrderID";
            orderIdParam.Value = orderId;

            command.Parameters.Add(orderIdParam);

            var dataAdapter = _providerFactory.CreateDataAdapter();
            var table = new DataTable();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                customerOrderDetails.Add(SqlMapper.Map<CustomerOrderDetails>(row));
            }

            EndExecution(command);

            return customerOrderDetails;
        }

        public IEnumerable<CustomerOrderHistory> GetCustomerOrderHistory(string customerId)
        {
            var customerOrderHistory = new List<CustomerOrderHistory>();
            var command = StartExecution();

            command.CommandText = "[CustOrderHist]";
            command.CommandType = CommandType.StoredProcedure;

            var customerIdParam = command.CreateParameter();
            customerIdParam.ParameterName = "@CustomerID";
            customerIdParam.Value = customerId;

            command.Parameters.Add(customerIdParam);

            var dataAdapter = _providerFactory.CreateDataAdapter();
            var table = new DataTable();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                customerOrderHistory.Add(SqlMapper.Map<CustomerOrderHistory>(row));
            }

            EndExecution(command);

            return customerOrderHistory;
        }

        private Order UpdateStatus(string commandText, int orderId)
        {
            var command = StartExecution();

            command.CommandText = commandText;

            var param = command.CreateParameter();
            param.ParameterName = "@id";
            param.Value = orderId;

            command.Parameters.Add(param);
            command.ExecuteNonQuery();

            EndExecution(command);

            return Get(orderId);
        }

        private DbCommand StartExecution()
        {
            _connection.Open();

            return _connection.CreateCommand();
        }

        private void EndExecution(DbCommand command)
        {
            command.Dispose();
            _connection.Close();
        }
    }
}
