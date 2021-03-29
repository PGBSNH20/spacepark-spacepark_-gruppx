using System;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Xunit;

namespace SpaceTests
{
    public class DbTests
    {
        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            using var db = new TestDatabase();
            using var context = db.CreateContext();

            Assert.True(await context.Database.CanConnectAsync());
        }
        
        [Fact]
        public void TablesShouldGetCreated()
        {
            using var db = new TestDatabase();
            using var context = db.CreateContext();

            Assert.Empty(context.Parkings);
            Assert.Empty(context.Payments);
        }

        [Fact]
        public void AddedItemShouldGetGeneratedId()
        {
            using var db = new TestDatabase();
            using var context = db.CreateContext();

            var newParking = new Parking() { Fee = 100, ShipName = "Test", ParkedBy = "Darth Vader", MaxLength = 10, Occupied = true };
            context.Parkings.Add(newParking);
            context.SaveChanges();

            Assert.Equal(1, context.Parkings.First().Id);
        }

        [Fact]
        public void PaymentShouldHaveName()
        {
            using var db = new TestDatabase();
            using var context = db.CreateContext();

            var pay = new Payment() {Amount = 100, PayDate = DateTime.Now, User = "Darth Vader"};
            context.Payments.Add(pay);
            context.SaveChanges();

            Assert.Equal("Darth Vader", context.Payments.First(x => x.Id == 1).User);

        }
    }
}
