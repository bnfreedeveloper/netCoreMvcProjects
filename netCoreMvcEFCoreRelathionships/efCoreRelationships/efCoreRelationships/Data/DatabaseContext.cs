using efCoreRelationships.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace efCoreRelationships.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           var student = modelBuilder.Entity<Student>();
          student.HasOne(student => student.Location).WithOne(adress =>adress.Student).HasForeignKey<StudentAdress>(adress => adress.StudentId);
            //if we we consider 
            modelBuilder.Entity<StudentAdress>(StudentAdress =>
            {
                StudentAdress.HasIndex(s => s.StudentId).IsUnique();
            });
        }
        public DbSet<Categorie>Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Student> Students { get; set; }    
        public DbSet<StudentAdress> StudentAdresses { get; set; } 
        
        public DbSet<Book> Books { get; set; }
    }
}
