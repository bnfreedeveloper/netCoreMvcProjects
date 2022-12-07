using ImageUploadRetrieve.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageUploadRetrieve.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ImageModel> Images { get; set; }
    }
}
