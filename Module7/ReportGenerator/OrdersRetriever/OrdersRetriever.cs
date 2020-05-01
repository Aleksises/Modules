using ReportGenerator.QueryParser;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ReportGenerator.OrdersRetriever
{
    public class OrdersRetriever : IOrdersRetriever
    {
        private readonly string _connectionString;

        public OrdersRetriever(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        public List<Order> GetOrders(Dictionary<string, string> parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            
            var command = BuildSqlCommand(parameters, connection);
            using var reader = command.ExecuteReader();

            var orders = new List<Order>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        CustomerId = reader.GetString(0),
                        OrderDate = reader.GetDateTime(1),
                        Freight = reader.GetDecimal(2),
                        ShipName = reader.GetString(3),
                        ShipCountry = reader.GetString(4)
                    });
                }
            }

            return orders;
        }

        private SqlCommand BuildSqlCommand(Dictionary<string, string> parameters, SqlConnection connection)
        {
            var sqlCommand = new SqlCommand
            {
                Connection = connection
            };

            var commandText = "SELECT Orders.[CustomerID], Orders.[OrderDate], Orders.[Freight], Orders.[ShipName], Orders.[ShipCountry] FROM [dbo].[Orders] AS Orders ";
            commandText += "WHERE 1=1 ";

            if (parameters.ContainsKey(QueryParametersConstants.CustomerIdParameter))
            {
                commandText += "AND Orders.[CustomerID] = @customerId ";
                sqlCommand.Parameters.AddWithValue("@customerId", parameters[QueryParametersConstants.CustomerIdParameter]);
            }
            if (parameters.ContainsKey(QueryParametersConstants.DateFromParameter))
            {
                commandText += "AND Orders.[OrderDate] >= @dateFrom ";
                sqlCommand.Parameters.AddWithValue("@dateFrom", parameters[QueryParametersConstants.DateFromParameter]);
            }
            else if (parameters.ContainsKey(QueryParametersConstants.DateToParameter))
            {
                commandText += "AND Orders.[OrderDate] <= @dateTo ";
                sqlCommand.Parameters.AddWithValue("@dateTo", parameters[QueryParametersConstants.DateToParameter]);
            }

            commandText += "ORDER BY Orders.[OrderID] ";
            
            if (parameters.ContainsKey(QueryParametersConstants.SkipParameter))
            {
                commandText += "OFFSET @skip ";
                sqlCommand.Parameters.AddWithValue("@skip", parameters[QueryParametersConstants.SkipParameter]);
            }
            if (parameters.ContainsKey(QueryParametersConstants.TakeParameter))
            {
                commandText += "FETCH NEXT @take ROWS ONLY";
                sqlCommand.Parameters.AddWithValue("@take", parameters[QueryParametersConstants.TakeParameter]);
            }

            sqlCommand.CommandText = commandText;

            return sqlCommand;
        }
    }
}
