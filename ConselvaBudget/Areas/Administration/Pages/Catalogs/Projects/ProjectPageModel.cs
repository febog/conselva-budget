using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Catalogs.Projects
{
    public class ProjectPageModel : PageModel
    {
        public SelectList DonorSL { get; set; }

        public void PopulateDonorDropDownList(ConselvaBudgetContext context,
            object selectedDonor = null)
        {
            var donorsQuery = context.Donors.OrderBy(d => d.ShortName);

            DonorSL = new SelectList(donorsQuery.AsNoTracking(),
                "Id",
                "ShortName",
                selectedDonor);
        }
    }
}
