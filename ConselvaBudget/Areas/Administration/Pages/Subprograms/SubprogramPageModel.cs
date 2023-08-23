using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Subprograms
{
    public class SubprogramPageModel : PageModel
    {
        public SelectList ProgramNameSL { get; set; }
        public List<AccountAssignmentData> AccountAssignmentDataList;

        public void PopulateDepartmentsDropDownList(ConselvaBudgetContext context, object selectedProgram = null)
        {
            var programsQuery = context.BusinessPrograms
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

        public void PopulateAccountAssignmentData(ConselvaBudgetContext context, BusinessSubprogram subprogram)
        {
            var subprogramAccounts = new HashSet<int>(subprogram.AccountAssignments.Select(a => a.AccountId));
            AccountAssignmentDataList = new List<AccountAssignmentData>();
            foreach (var account in context.Accounts.OrderBy(a => a.Code))
            {
                AccountAssignmentDataList.Add(new AccountAssignmentData
                {
                    AccountId = account.Id,
                    AccountName = account.DisplayName,
                    Assigned = subprogramAccounts.Contains(account.Id)
                });
            }
        }

        public class AccountAssignmentData
        {
            public int AccountId { get; set; }
            public string AccountName { get; set; }
            public bool Assigned { get; set; }
        }
    }
}
