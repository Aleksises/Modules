using ReportGenerator.QueryParser;
using System.Collections.Generic;
using System.Net.Http;

namespace ReportGenerator.ParametersRetriever
{
    public class HttpRequestParametersRetriever : IHttpRequestParametersRetriever
    {
        private readonly IQueryParser _parser;

        public HttpRequestParametersRetriever(IQueryParser queryParser)
        {
            _parser = queryParser;
        }

        public Dictionary<string, string> GetParameters(HttpRequestMessage request)
        {
            if (!string.IsNullOrEmpty(request.RequestUri.Query))
            {
                return _parser.ParseQueryString(request.RequestUri.Query);
            }

            var content = request.Content.ReadAsStringAsync().Result;

            return _parser.ParseQueryString(content);
        }
    }
}
