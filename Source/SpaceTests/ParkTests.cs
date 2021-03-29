using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace SpaceTests
{
    
    public class ParkTests
    {
        [Fact]
        public void Size_Should_Be_Too_Big()
        {
            // Prepare mock database
            using var db = new TestDatabase();
            using var context = db.CreateContext();
            var newParking = new Parking() { MaxLength = 10 };
            context.Parkings.Add(newParking);
            context.SaveChanges();

            var check = new ParkingChecks();
            var parking = context.Parkings.ToListAsync();
            var ship = new Starship {Length = 30};

            bool result = check.SizeTooBig(parking, 0, ship);

            Assert.True(result);
        }
    }
}
