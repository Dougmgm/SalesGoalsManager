using Microsoft.EntityFrameworkCore;

namespace SalesGoalsManager.Api.Data
{
    public class SalesGoalsManagerDbContext : DbContext
    {
        public SalesGoalsManagerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        //public DbSet<Difficulty> Difficulties { get; set; }

        //public DbSet<Region> Regions { get; set; }

        //public DbSet<Walk> Walks { get; set; }
    }
}
