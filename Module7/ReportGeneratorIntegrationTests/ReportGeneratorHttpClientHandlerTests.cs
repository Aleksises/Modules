using NUnit.Framework;
using ReportGenerator;
using ReportGenerator.OrdersRetriever;
using ReportGenerator.ParametersRetriever;
using ReportGenerator.QueryParser;
using ReportGenerator.Reports;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
            var reportGeneratorService = new ReportGeneratorService();
            using var handler = new ReportGeneratorHttpClientHandler(parametersRetriever, ordersRetriever, reportGeneratorService);
            _client = new HttpClient(handler);
        }

        [TearDown]
        public void TearDown()
        {
            _client.DefaultRequestHeaders.Clear();
        }

        [Test]
        public async Task ReportGeneratorHandler_WhenParamsAreProvidedByQueryStringWithXmlHeader_ShouldReturnResponseWithReportPathAndXmlMediaType()
        {
            // Arrange
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            
            // Act
            var response = await _client.GetAsync("http://localhost/Report?customerId=CHOPS&dateFrom=1998-03-20");
            var reportPath = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.IsNotEmpty(reportPath);
            Assert.AreEqual(response.Content.Headers.ContentType.MediaType, "text/xml");
        }

        [Test]
        public async Task ReportGeneratorHandler_WhenParamsAreProvidedByQueryStringWithExcelHeader_ShouldReturnResponseWithReportPathAndExcelMediaType()
        {
            // Arrange
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));

            // Act
            var response = await _client.GetAsync("http://localhost/Report?customerId=CHOPS&dateFrom=1998-03-20");
            var reportPath = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.IsNotEmpty(reportPath);
            Assert.AreEqual(response.Content.Headers.ContentType.MediaType, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Test]
        public async Task ReportGeneratorHandler_WhenParamsAreProvidedByQueryStringWithoutMediaTypeHeader_ShouldReturnResponseWithReportPathAndExcelMediaType()
        {
            // Act
            var response = await _client.GetAsync("http://localhost/Report?customerId=CHOPS&dateFrom=1998-03-20");
            var reportPath = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.IsNotEmpty(reportPath);
            Assert.AreEqual(response.Content.Headers.ContentType.MediaType, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Test]
        public async Task ReportGeneratorHandler_WhenParamsAreProvidedByBodyWithXmlHeader_ShouldReturnResponseWithReportPathAndXmlMediaType()
        {
            // Arrange
            var content = new StringContent("customerId=CHOPS&dateFrom=1998-03-20", Encoding.UTF8, "application/x-www-form-urlencoded");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            
            // Act
            var response = await _client.PostAsync("http://localhost/Report", content);
            var reportPath = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.IsNotEmpty(reportPath);
            Assert.AreEqual(response.Content.Headers.ContentType.MediaType, "text/xml");
        }

        [Test]
        public async Task ReportGeneratorHandler_WhenParamsAreProvidedByBodyWithExcelHeader_ShouldReturnResponseWithReportPathAndExcelMediaType()
        {
            // Arrange
            var content = new StringContent("customerId=CHOPS&dateFrom=1998-03-20", Encoding.UTF8, "application/x-www-form-urlencoded");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));

            // Act
            var response = await _client.PostAsync("http://localhost/Report", content);
            var reportPath = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.IsNotEmpty(reportPath);
            Assert.AreEqual(response.Content.Headers.ContentType.MediaType, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Test]
        public async Task ReportGeneratorHandler_WhenParamsAreProvidedByBodyWithoutMediaTypeHader_ShouldReturnResponseWithReportPathAndExcelMediaType()
        {
            // Arrange
            var content = new StringContent("customerId=CHOPS&dateFrom=1998-03-20", Encoding.UTF8, "application/x-www-form-urlencoded");

            // Act
            var response = await _client.PostAsync("http://localhost/Report", content);
            var reportPath = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.IsNotEmpty(reportPath);
            Assert.AreEqual(response.Content.Headers.ContentType.MediaType, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Test]
        public void ReportGeneratorHandler_WhenParamsRepeat_ShouldThrowArgumentException()
        {
            // Arrange
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

            // Act/Assert
            Assert.Throws(typeof(ArgumentException), () => _client.GetAsync("http://localhost/Report?customerId=CHOPS&dateFrom=1998-03-20&dateFrom=2019-01-01"));
        }

        [Test]
        public void ReportGeneratorHandler_WhenParamsHaveDateFromAndDateTo_ShouldThrowArgumentException()
        {
            // Arrange
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

            // Act/Assert
            Assert.Throws(typeof(ArgumentException), () => _client.GetAsync("http://localhost/Report?customerId=CHOPS&dateFrom=1998-03-20&dateTo=2019-01-01"));
        }
    }
}