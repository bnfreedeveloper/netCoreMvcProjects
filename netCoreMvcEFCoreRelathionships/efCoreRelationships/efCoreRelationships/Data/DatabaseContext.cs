using efCoreRelationships.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace efCoreRelationships.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) :base(options)
        {

        }
        public DbSet<Categorie>Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
