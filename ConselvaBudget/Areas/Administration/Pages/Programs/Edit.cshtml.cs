﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Programs
{
    public class EditModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public EditModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BusinessProgram BusinessProgram { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BusinessPrograms == null)
            {
                return NotFound();
            }

            var businessprogram =  await _context.BusinessPrograms.FirstOrDefaultAsync(m => m.Id == id);
            if (businessprogram == null)
            {
                return NotFound();
            }
            BusinessProgram = businessprogram;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BusinessProgram).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessProgramExists(BusinessProgram.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BusinessProgramExists(int id)
        {
          return (_context.BusinessPrograms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
