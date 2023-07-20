using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
      public AccountCategory AccountCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AccountCategories == null)
            {
                return NotFound();
            }

            var accountcategory = await _context.AccountCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (accountcategory == null)
            {
                return NotFound();
            }
            else 
            {
                AccountCategory = accountcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.AccountCategories == null)
            {
                return NotFound();
            }
            var accountcategory = await _context.AccountCategories.FindAsync(id);

            if (accountcategory != null)
            {
                AccountCategory = accountcategory;
                _context.AccountCategories.Remove(AccountCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
