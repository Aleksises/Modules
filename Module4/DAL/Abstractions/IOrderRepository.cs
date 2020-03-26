using DAL.Models;
using System.Collections.Generic;

namespace DAL.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order SetOrderedDate(int orderId);

        Order SetDone(int orderId);

        IEnumerable<CustomerOrderDetails> GetCustomerOrderDetails(int orderId);

        IEnumerable<CustomerOrderHistory> GetCustomerOrderHistory(string customerId);
    }
}
