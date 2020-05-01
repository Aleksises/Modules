using ReportGenerator.OrdersRetriever;
using System.Collections.Generic;

namespace ReportGenerator.Reports
{
    public interface IReportGeneratorService
    {
        string GenerateExcelReport(List<Order> orders);

        string GenerateXmlReport(List<Order> orders);
    }
}
