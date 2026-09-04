using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SalesGoalManager.RegraDeNegocio.Contexto;

namespace SalesGoalManager.RegraDeNegocio
{
    public class ContextoFactoryDesignTime : IDesignTimeDbContextFactory<SalesGoalsManagerDbContext>
    {
        public SalesGoalsManagerDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<SalesGoalsManagerDbContext>()
                .UseSqlServer(Fabrica.ConnectionString)
                .Options;

            return new SalesGoalsManagerDbContext(options);
        }
    }
}