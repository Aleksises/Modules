using System.Collections.Generic;

namespace ReportGenerator.OrdersRetriever
{
    public interface IOrdersRetriever
    {
        List<Order> GetOrders(Dictionary<string, string> parameters);
    }
}
