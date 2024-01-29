using ConselvaBudget.Data;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Seed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting database seed process...");

#if DEBUG
            var options = new DbContextOptionsBuilder<ConselvaBudgetContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ConselvaBudgetContext-e30d848b-d770-45f5-aecc-8efa6536e102;Trusted_Connection=True;MultipleActiveResultSets=true").Options;

            Data.Initialize(options);
#endif

            Console.WriteLine("Database seeded!");
        }
    }
}
