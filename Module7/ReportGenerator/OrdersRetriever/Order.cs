using System;

namespace ReportGenerator.OrdersRetriever
{
    public class Order
    {
        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public double Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipCountry { get; set; }
    }
}
