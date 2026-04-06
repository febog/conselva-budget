using Microsoft.AspNetCore.Mvc;

namespace ConselvaBudget.Services
{
    public interface IReportService<T>
    {
        /// <summary>
        /// Creates a report download for the tabular data given.
        /// </summary>
        /// <returns></returns>
        FileContentResult GenerateExcelFileDownload(IList<T> data, string name = null);
    }
}
