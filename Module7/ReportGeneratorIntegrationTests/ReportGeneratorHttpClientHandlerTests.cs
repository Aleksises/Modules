using NUnit.Framework;
using ReportGenerator;
using ReportGenerator.OrdersRetriever;
using ReportGenerator.ParametersRetriever;
using ReportGenerator.QueryParser;
using System.Net.Http;

namespace ReportGeneratorIntegrationTests
{
    public class ReportGeneratorHttpClientHandlerTests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var parametersRetriever = new HttpRequestParametersRetriever(new QueryStringParser());
            var ordersRetriever = new OrdersRetriever(@"Server=(local);Database=Northwind;Trusted_Connection = true");
            using var handler = new ReportGeneratorHttpClientHandler(parametersRetriever, ordersRetriever);
            _client = new HttpClient(handler);
        }

        [Test]
        public void Test1()
        {
            //_client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            _client.GetAsync("http://localhost/Report?customerId=CHOPS&dateFrom=2019-03-20");
        }
    }
}