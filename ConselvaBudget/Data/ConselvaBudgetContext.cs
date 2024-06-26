﻿using ConselvaBudget.Models;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Data
{
    public class ConselvaBudgetContext : DbContext
    {
        public ConselvaBudgetContext(DbContextOptions<ConselvaBudgetContext> options)
            : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<AccountAssignment> AccountAssignments { get; set; }

        public DbSet<Donor> Donors { get; set; }

        public DbSet<EquivalentAccount> EquivalentAccounts { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<ActivityBudget> ActivityBudgets { get; set; }

        public DbSet<ActivityBudgetLogEntry> ActivityBudgetLogEntries { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<RequestLogEntry> RequestLogEntries { get; set; }

        public DbSet<AmountRequest> AmountRequests { get; set; }

        public DbSet<ExpenseInvoice> ExpenseInvoices { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Deposit> Deposits { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<NotificationRecipient> NotificationRecipients { get; set; }
    }
}
