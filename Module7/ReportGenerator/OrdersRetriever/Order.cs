using System;

namespace ReportGenerator.OrdersRetriever
{
    [Serializable]
    public class Order
    {
        public Order()
        {
        }

        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipCountry { get; set; }
    }
}
