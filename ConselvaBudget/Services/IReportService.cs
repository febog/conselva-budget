using Microsoft.AspNetCore.Mvc;

namespace ConselvaBudget.Services
{
    public interface IReportService
    {
        /// <summary>
        /// Creates a report download for the tabular data given.
        /// </summary>
        /// <returns></returns>
        FileContentResult GenerateExcelFileDownload<T>(IList<T> data, string name = null);
    }
}
