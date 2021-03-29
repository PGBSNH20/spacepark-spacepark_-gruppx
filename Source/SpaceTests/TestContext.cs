using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace SpaceTests
{
    public class TestContext : DbContext
    {
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<Payment> Payments { get; set; }


        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }
    }
}
