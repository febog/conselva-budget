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
        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountAssignment> AccountAssignments { get; set; }

        public DbSet<Donor> Donors { get; set; }

        public DbSet<EquivalentAccount> EquivalentAccounts { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<ActivityBudget> ActivityBudgets { get; set; }

        public DbSet<SpendingRequest> SpendingRequests { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Deposit> Deposits { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
