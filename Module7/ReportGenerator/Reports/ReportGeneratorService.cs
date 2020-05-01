using ClosedXML.Excel;
using ReportGenerator.OrdersRetriever;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ReportGenerator.Reports
{
    public class ReportGeneratorService : IReportGeneratorService
    {
        private const int StartRow = 1;
        private const int ColumnlOffset = 1;
        private const int RowOffset = 1;

        private readonly string _directoryPath;

        private readonly List<string> _orderHeaders = new List<string>
        {
            "Customer Id",
            "Order Date",
            "Freight",
            "Ship Name",
            "Ship Country"
        };

        public ReportGeneratorService()
        {
            _directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }

        public string GenerateExcelReport(List<Order> orders)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("OrdersReport");
            for (int i = 0; i < _orderHeaders.Count; i++)
            {
                worksheet.Cell(StartRow, ColumnlOffset + i).Value = _orderHeaders[i];
                worksheet.Cell(StartRow, ColumnlOffset + i).Style.Fill.BackgroundColor = XLColor.Orange;
                worksheet.Cell(StartRow, ColumnlOffset + i).Style.Font.Bold = true;
            }
            worksheet.Columns(1, _orderHeaders.Count).Style.Fill.BackgroundColor = XLColor.Orange;

            for (int i = 0; i < orders.Count; i++)
            {
                var rowNumber = StartRow + RowOffset + i;
                worksheet.Cell(rowNumber, 1).Value = orders[i].CustomerId;
                worksheet.Cell(rowNumber, 2).Value = orders[i].OrderDate;
                worksheet.Cell(rowNumber, 3).Value = orders[i].Freight;
                worksheet.Cell(rowNumber, 4).Value = orders[i].ShipName;
                worksheet.Cell(rowNumber, 5).Value = orders[i].ShipCountry;
            }

            var filePath = Path.Combine(_directoryPath, "ExcelOrdersReport.xlsx");
            workbook.SaveAs(filePath);

            return filePath;
        }

        public string GenerateXmlReport(List<Order> orders)
        {
            var serializer = new XmlSerializer(typeof(List<Order>));

            var filePath = Path.Combine(_directoryPath, "XmlOrdersReport.xml");
            using var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            serializer.Serialize(fileStream, orders);

            return filePath;
        }
    }
}
