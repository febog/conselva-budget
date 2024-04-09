using System.Security.Claims;
using ConselvaBudget.Authorization;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<ExpensesRequest> SpendingRequests { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var requests = _context.SpendingRequests.Select(r => r);

            if (!User.IsInRole(Roles.Management))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                requests = requests.Where(r => r.RequestorUserId == userId);
            }

            SpendingRequests = await requests
                .Include(r => r.Activity)
                .ToListAsync();
        }
    }
}
