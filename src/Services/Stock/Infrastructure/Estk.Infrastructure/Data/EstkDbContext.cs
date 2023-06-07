using Estk.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Estk.Infrastructure.Data
{
    public class EstkDbContext : DbContext
    {
        public EstkDbContext(DbContextOptions<EstkDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
                .Property(p => p.RowVersion).IsConcurrencyToken();
        }
    }
}
