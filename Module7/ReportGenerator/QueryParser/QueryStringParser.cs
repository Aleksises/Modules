using System.Collections.Generic;
using System.Web;

namespace ReportGenerator.QueryParser
{
    public class QueryStringParser : IQueryParser
    {
        public Dictionary<string, string> ParseQueryString(string query)
        {
            var queryParameters = HttpUtility.ParseQueryString(query);

            var parsedParameters = new Dictionary<string, string>();

            foreach (var validParameter in QueryParametersConstants.ValidParameters)
            {
                var parameterValue = queryParameters.Get(validParameter);
                if (parameterValue != null)
                {
                    parsedParameters.Add(validParameter, parameterValue);
                }
            }

            return parsedParameters;
        }
    }
}
