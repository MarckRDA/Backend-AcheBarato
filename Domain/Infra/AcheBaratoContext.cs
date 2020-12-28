using Domain.Models.Cathegories;
using Domain.Models.Products;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Domain.Infra
{
    public class AcheBaratoContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<Cathegory> Cathegories { get; set; }
        public DbSet<CathegoryChild> CathegoryChildren { get; set; }
        public DbSet<AlarmPrice> AlarmPrices { get; set; }
        public DbSet<HistorycalPrice> HistoricalPrices { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;User Id=sa;PWD=some(!)Password;Initial Catalog=AcheBarato");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly()
            );
        }
    }
}