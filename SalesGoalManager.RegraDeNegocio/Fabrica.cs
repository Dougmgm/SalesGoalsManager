// RegraDeNegocio/Fabrica.cs
using Microsoft.EntityFrameworkCore;
using SalesGoalManager.RegraDeNegocio.Cadastros;
using SalesGoalManager.RegraDeNegocio.Consultas;
using SalesGoalManager.RegraDeNegocio.Contexto;
using SalesGoalManager.RegraDeNegocio.Repositorios;
using SalesGoalManager.RegraDeNegocio.Validacoes;

namespace SalesGoalManager.RegraDeNegocio
{
    public static class Fabrica
    {
        public const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=SalesGoalsManagerDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SalesGoalsManagerDbContext CriarContexto()
        {
            var options = new DbContextOptionsBuilder<SalesGoalsManagerDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new SalesGoalsManagerDbContext(options);
        }

        public static MetaConsulta CriarMetaConsulta()
            => new MetaConsulta(new MetaRepositorio(CriarContexto()));

        public static MetaCadastro CriarMetaCadastro()
            => new MetaCadastro(new MetaRepositorio(CriarContexto()), new MetaVendedorValidacao());

        public static ProdutoConsulta CriarProdutoConsulta()
            => new ProdutoConsulta(new ProdutoRepositorio(CriarContexto()));

        public static VendedorConsulta CriarVendedorConsulta()
            => new VendedorConsulta(new VendedorRepositorio(CriarContexto()));
    }
}