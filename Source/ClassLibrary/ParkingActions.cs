using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Api;
using ClassLibrary.Models;

namespace ClassLibrary
{
    public class ParkingActions
    {
        public void Park(Starship ship)
        {
            var parkingCheck = new ParkingChecks();
            StandardMessages.LoadingMessage();
            var parkings = parkingCheck.ParkingLots();
            var array = ArrayBuilder.ParkArray(parkings); // Create string array of all parkings to present to the user in the menu
            Console.Clear();
            var selectedOption = Menu.ShowMenu($"You have selected to park {ship.Name}. Choose parking spot", array);

            Finish(parkings, selectedOption, ship); // Add ship to parking in database
        }
        private static void Finish(Task<List<Parking>> parkings, int index, Starship ship) // Final check before adding to database
        {
            var parkingCheck = new ParkingChecks();
            if (parkingCheck.SizeTooBig(parkings, index, ship))
            {
                Console.WriteLine("Too big.");
                Console.ReadKey();
            }
            else
            {
                if (Occupation.ParkIsOccupied(parkings, index))
                {
                    Console.WriteLine("Occupied");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    using var context = new SpaceContext();
                    Console.WriteLine("Parking..");
                    var park = context.Parkings.First(p => p.Id == parkings.Result[index].Id);
                    park.Occupied = true;
                    park.ShipName = ship.Name;
                    park.ParkedBy = ship.Driver;
                    context.SaveChangesAsync();
                    Console.WriteLine("Parked");
                    Console.ReadKey();
                }
            }
        }
    }
}

