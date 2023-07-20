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
    public class DetailsModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public DetailsModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

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
    }
}
