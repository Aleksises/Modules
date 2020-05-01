using System.Collections.Generic;

namespace ReportGenerator.QueryParser
{
    internal static class QueryParametersConstants
    {
        internal const string CustomerIdParameter = "customerId";
        internal const string DateFromParameter = "dateFrom";
        internal const string DateToParameter = "dateTo";
        internal const string TakeParameter = "take";
        internal const string SkipParameter = "skip";

        internal static readonly List<string> ValidParameters = new List<string>
        {
            CustomerIdParameter,
            DateFromParameter,
            DateToParameter,
            TakeParameter,
            SkipParameter
        };
    }
}
