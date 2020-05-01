using System.Collections.Generic;

namespace ReportGenerator.QueryParser
{
    public interface IQueryParser
    {
        Dictionary<string, string> ParseQueryString(string query);
    }
}
