using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace ConselvaBudget.Services
{
    public class ExcelReportService : IReportService
    {
        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string FileExtension = ".xlsx";

        public FileContentResult GenerateExcelFileDownload<T>(IList<T> data, string name = null)
        {
            // Generate Excel file content
            byte[] excelFile = GenerateExcelFile<T>(data);

            return new FileContentResult(excelFile, ContentType)
            {
                FileDownloadName = (string.IsNullOrEmpty(name) ? "Report" : name) + FileExtension
            };
        }

        private byte[] GenerateExcelFile<T>(IList<T> data)
        {
            ExcelPackage.License.SetNonCommercialOrganization("Conselva");
            using (var package = new ExcelPackage())
            {
                // Add a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Data");

                // Add headers
                var properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    // Populate first row with the property names.
                    worksheet.Cells[1, i + 1].Value = properties[i].Name;
                }

                // Add data
                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < properties.Length; j++)
                    {
                        var item = data[i];
                        var property = properties[j];
                        worksheet.Cells[i + 2, j + 1].Value = property.GetValue(item);
                    }
                }

                // Convert the package to a byte array
                return package.GetAsByteArray();
            }
        }
    }
}
