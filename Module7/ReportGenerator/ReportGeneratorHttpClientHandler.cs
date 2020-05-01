using ReportGenerator.OrdersRetriever;
using ReportGenerator.ParametersRetriever;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ReportGenerator
{
    public class ReportGeneratorHttpClientHandler : HttpClientHandler
    {
        private readonly IHttpRequestParametersRetriever _parametersRetriever;
        private readonly IOrdersRetriever _ordersRetriever;

        public ReportGeneratorHttpClientHandler(IHttpRequestParametersRetriever parametersRetriever,
            IOrdersRetriever ordersRetriever)
        {
            _parametersRetriever = parametersRetriever;
            _ordersRetriever = ordersRetriever;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsolutePath.TrimStart('/') != "Report")
            {
                return base.SendAsync(request, cancellationToken);
            }

            var parameters = _parametersRetriever.GetParameters(request);
            var orders = _ordersRetriever.GetOrders(parameters);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
