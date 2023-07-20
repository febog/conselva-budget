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
        public DbSet<BusinessProgram> BusinessPrograms { get; set; }

        public DbSet<BusinessSubprogram> BusinessSubprograms { get; set; }

        public DbSet<AccountCategory> AccountCategories { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Donor> Donors { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<ActivityBudget> ActivityBudgets { get; set; }

        public DbSet<Expense> Expenses { get; set; }

    }
}
