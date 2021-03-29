using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary
{
    public class SpaceContext : DbContext
    {
        public SpaceContext()
        { }
        public SpaceContext(DbContextOptions<SpaceContext> options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = SpacePark; Integrated Security = True;");
    //      optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=SpacePark;Integrated Security=True;");
        }

        public virtual DbSet<Parking> Parkings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parking>().HasData(
                new Parking() { Id = 1, Fee = 10, MaxLength = 50, Occupied = false },
                new Parking() { Id = 2, Fee = 50, MaxLength = 100, Occupied = false },
                new Parking() { Id = 3, Fee = 100, MaxLength = 200, Occupied = false },
                new Parking() { Id = 4, Fee = 1000, MaxLength = 2000, Occupied = false },
                new Parking() { Id = 5, Fee = 5, MaxLength = 15, Occupied = false });
        }

    }
}
