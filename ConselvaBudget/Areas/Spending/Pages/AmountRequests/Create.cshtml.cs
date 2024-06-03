using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Spending.Pages.AmountRequests
{
    public class CreateModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ActivityBudgetId"] = new SelectList(_context.ActivityBudgets, "Id", "Id");
        ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Description");
            return Page();
        }

        [BindProperty]
        public AmountRequest AmountRequest { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AmountRequests.Add(AmountRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
