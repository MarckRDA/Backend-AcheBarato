using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Domain.Infra
{
  public class BrasileiraoContext : DbContext
    {
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