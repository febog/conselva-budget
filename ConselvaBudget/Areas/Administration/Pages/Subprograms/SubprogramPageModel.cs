using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Subprograms
{
    public class SubprogramPageModel : PageModel
    {
        public SelectList ProgramNameSL { get; set; }

        public void PopulateDepartmentsDropDownList(ConselvaBudgetContext _context,
            object selectedProgram = null)
        {
            var programsQuery = _context.BusinessPrograms
                .OrderBy(p => p.Code)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.DisplayName
                });

            ProgramNameSL = new SelectList(programsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedProgram);
        }
    }
}
