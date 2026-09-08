using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SalesGoalsManager.RegraDeNegocio.Contexto;

namespace SalesGoalsManager.RegraDeNegocio
{
    public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<SalesGoalsManagerDbContext>
    {
        public SalesGoalsManagerDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<SalesGoalsManagerDbContext>()
                .UseSqlServer(ServiceFactory.ConnectionString)
                .Options;

            return new SalesGoalsManagerDbContext(options);
        }
    }
}