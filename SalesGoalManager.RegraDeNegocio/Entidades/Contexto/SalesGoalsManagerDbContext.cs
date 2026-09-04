using Microsoft.EntityFrameworkCore;
using SalesGoalManager.RegraDeNegocio.Entidades;

namespace SalesGoalManager.RegraDeNegocio.Contexto
{
    public class SalesGoalsManagerDbContext : DbContext
    {
        public SalesGoalsManagerDbContext(DbContextOptions<SalesGoalsManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Meta> Metas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendedor>(cfg =>
            {
                cfg.ToTable("Vendedor");
                cfg.Property(v => v.Nome).IsRequired().HasMaxLength(150);
            });

            modelBuilder.Entity<Produto>(cfg =>
            {
                cfg.ToTable("Produto");
                cfg.Property(p => p.NomeProduto).IsRequired().HasMaxLength(150);
                cfg.Property(p => p.Categoria).HasConversion<int>();
            });

            modelBuilder.Entity<Meta>(cfg =>
            {
                cfg.ToTable("Meta");
                cfg.Property(m => m.ValorMeta).HasColumnType("decimal(18,2)");
                cfg.Property(m => m.Periodicidade).HasConversion<int>();
                cfg.Property(m => m.TipoMeta).HasConversion<int>();

                cfg.HasOne(m => m.Vendedor)
                   .WithMany()
                   .HasForeignKey(m => m.VendedorId)
                   .OnDelete(DeleteBehavior.Restrict);

                cfg.HasOne(m => m.Produto)
                   .WithMany()
                   .HasForeignKey(m => m.ProdutoId)
                   .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}