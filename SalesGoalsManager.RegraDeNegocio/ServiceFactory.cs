using Microsoft.EntityFrameworkCore;
using SalesGoalsManager.RegraDeNegocio.Cadastro;
using SalesGoalsManager.RegraDeNegocio.Consultas;
using SalesGoalsManager.RegraDeNegocio.Contexto;
using SalesGoalsManager.RegraDeNegocio.Repositorios;
using SalesGoalsManager.RegraDeNegocio.Validacoes;

namespace SalesGoalsManager.RegraDeNegocio
{
    public static class ServiceFactory
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