using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using ConselvaBudget.Authorization;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Budget.Pages.Results
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Result Result { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CanDeleteResult(User))
            {
                return Forbid();
            }

            if (id == null || _context.Results == null)
            {
                return NotFound();
            }

            Result = await _context.Results
                .Include(r => r.Project)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Result == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!CanDeleteResult(User))
            {
                return Forbid();
            }

            if (id == null || _context.Results == null)
            {
                return NotFound();
            }
            var result = await _context.Results.FindAsync(id);

            if (result != null)
            {
                Result = result;
                _context.Results.Remove(Result);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Projects/Manage", new { id = result.ProjectId });
        }

        private bool CanDeleteResult(ClaimsPrincipal user)
        {
            return user.IsInRole(Roles.Management);
        }
    }
}
