using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Organizations
{
    public class OrganizationPageModel : PageModel
    {
        public List<AccountAssignmentData> AccountAssignmentDataList;

        public void PopulateAccountAssignmentData(ConselvaBudgetContext context, Organization organization)
        {
            var organizationAccounts = new HashSet<int>(organization.AccountAssignments.Select(a => a.AccountId));
            AccountAssignmentDataList = new List<AccountAssignmentData>();
            foreach (var account in context.Accounts.OrderBy(a => a.Code))
            {
                AccountAssignmentDataList.Add(new AccountAssignmentData
                {
                    AccountId = account.Id,
                    AccountName = account.DisplayName,
                    Assigned = organizationAccounts.Contains(account.Id)
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
