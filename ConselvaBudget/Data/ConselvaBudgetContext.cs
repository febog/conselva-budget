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

        public DbSet<ConselvaBudget.Models.Organization> Organization { get; set; } = default!;
    }
}
