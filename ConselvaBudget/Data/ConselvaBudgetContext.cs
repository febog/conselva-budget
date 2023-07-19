using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Models;

namespace ConselvaBudget.Data
{
    public class ConselvaBudgetContext : DbContext
    {
        public ConselvaBudgetContext (DbContextOptions<ConselvaBudgetContext> options)
            : base(options)
        {
        }
        public DbSet<BusinessProgram> BusinessPrograms { get; set; } = default!;

        public DbSet<BusinessSubprogram> BusinessSubprograms { get; set; } = default!;

        public DbSet<AccountCategory> AccountCategories { get; set; } = default!;

        public DbSet<Account> Accounts { get; set; } = default!;

        public DbSet<Project> Projects { get; set; } = default!;

        public DbSet<Result> Results { get; set; } = default!;

        public DbSet<Activity> Activities { get; set; } = default!;

        public DbSet<ActivityBudget> ActivityBudgets { get; set; } = default!;

        public DbSet<Expense> Expenses { get; set; } = default!;

    }
}
