using System.Collections.Generic;
using System.Net.Http;

namespace ReportGenerator.ParametersRetriever
{
    public interface IHttpRequestParametersRetriever
    {
        Dictionary<string, string> GetParameters(HttpRequestMessage request);
    }
}
