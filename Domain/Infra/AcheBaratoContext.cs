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
        public DbSet<Product> Product { get; set; }
        public DbSet<HistorycalPrice> HistoricalPrices { get; set; }
        public DbSet<Cathegory> Cathegorys { get; set; }
        
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