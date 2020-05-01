using ReportGenerator.OrdersRetriever;
using ReportGenerator.ParametersRetriever;
using ReportGenerator.QueryParser;
using ReportGenerator.Reports;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReportGenerator
{
    public class ReportGeneratorHttpClientHandler : HttpClientHandler
    {
        private readonly MediaTypeWithQualityHeaderValue _xmlTextAcceptHeader = new MediaTypeWithQualityHeaderValue("text/xml");
        private readonly MediaTypeWithQualityHeaderValue _xmlApplicationAcceptHeader = new MediaTypeWithQualityHeaderValue("application/xml");

        private readonly IHttpRequestParametersRetriever _parametersRetriever;
        private readonly IOrdersRetriever _ordersRetriever;
        private readonly IReportGeneratorService _reportGenerator;

        public ReportGeneratorHttpClientHandler(IHttpRequestParametersRetriever parametersRetriever,
            IOrdersRetriever ordersRetriever,
            IReportGeneratorService reportGenerator)
        {
            _parametersRetriever = parametersRetriever;
            _ordersRetriever = ordersRetriever;
            _reportGenerator = reportGenerator;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsolutePath.TrimStart('/') != "Report")
            {
                return base.SendAsync(request, cancellationToken);
            }

            var parameters = _parametersRetriever.GetParameters(request);

            if (parameters.Values.Any(x => x.Contains(',')))
            {
                throw new ArgumentException("You must provide only one unique parameter");
            }
            if (parameters.Keys.Contains(QueryParametersConstants.DateFromParameter) && parameters.Keys.Contains(QueryParametersConstants.DateToParameter))
            {
                throw new ArgumentException("You can provide only dateFrom or dateTo parameter");
            } 

            var orders = _ordersRetriever.GetOrders(parameters);

            var isXmlAcceptHeader = request.Headers.Accept.Contains(_xmlTextAcceptHeader) || request.Headers.Accept.Contains(_xmlApplicationAcceptHeader);
            var reportPath = isXmlAcceptHeader
                ? _reportGenerator.GenerateXmlReport(orders)
                : _reportGenerator.GenerateExcelReport(orders);

            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(reportPath, Encoding.UTF8, isXmlAcceptHeader ? "text/xml" : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            };

            return Task.FromResult(response);
        }
    }
}
