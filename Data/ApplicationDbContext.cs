using ProductManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace ProductManager.Data;

class ApplicationDbContext : DbContext
{
  private string connectionString = "Server=localhost;Database=ProductManager;User Id=SA;Password=12125936An;Encrypt=False;";

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(connectionString);
  }

  public DbSet<Product> Product { get; set; }
}