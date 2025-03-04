using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nome).IsRequired().HasMaxLength(30);
                entity.Property(p => p.Preco).HasColumnName("numeric(18,2)");
            });
        }
    }
}
