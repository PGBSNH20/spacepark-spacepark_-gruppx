using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Api;
using ClassLibrary.Models;

namespace ClassLibrary
{
    public class LeaveParkingActions
    {
        public void LeavePark() // Read name and let user select the ship to leave with
        {
            var parkingCheck = new ParkingChecks();
            var person = new PersonApi();
            var payment = new PaymentActions();
            string name = StandardMessages.NameReader();
            var r = person.GetAllPersons();
            StandardMessages.LoadingMessage();
            if (r.Result.Any(p => p.Name == name))
            {
                var parkings = parkingCheck.ParkingLots();
                var array = ArrayBuilder.OnLeaveArray(parkings);
                Console.Clear();
                var selectedOption = Menu.ShowMenu($"Welcome {name}. What ship will you be leaving with today?\n", array);

                if (Occupation.ParkIsOccupied(parkings, selectedOption) == false) // If user selects a empty parking lot, display message
                    StandardMessages.EmptyParkingLotMessage();
                else if (parkings.Result[selectedOption].ParkedBy != name) // If user selects a ship that is not theirs, display message
                    StandardMessages.NotYourShipMessage();
                else
                {
                    payment.Pay(parkings, selectedOption, name); // Pay for the parking lot by getting the database row
                    Leave(parkings, selectedOption); // Remove ship from database
                }
            }
            else
            {
                StandardMessages.NotAllowedMessage();
            }
        }
        private static void Leave(Task<List<Parking>> parkings, int index) // Remove entry from database
        {
            using var context = new SpaceContext();
            Console.WriteLine("Leaving...");
            var ship = context.Parkings.First(s => s.Id == parkings.Result[index].Id);
            ship.Occupied = false;
            ship.ShipName = null;
            ship.ParkedBy = null;
            context.SaveChangesAsync();
            Console.Clear();
            Console.WriteLine("Left.");
            Console.ReadKey();
        }
    }
}
