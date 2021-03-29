using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsoleApp
{
    class MyContext : DbContext
    {
        public DbSet<People> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=localhost,31433;Database=CodeFirst;User Id=sa;Password=verystrong!pass123;");
        }
    }
}
