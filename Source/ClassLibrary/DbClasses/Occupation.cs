using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary
{
    public class Occupation
    {
        public static bool AllParksOccupied() // Check if all parkings are occupied
        {
            using var context = new SpaceContext();
            StandardMessages.LoadingMessage();
            if (!context.Parkings.All(i => i.Occupied)) return false;
            StandardMessages.FullParkMessage();
            return true;
        }
        public static bool ParkIsOccupied(Task<List<Parking>> parkings, int index) // Check if specific park is occupied
        {
            using var context = new SpaceContext();
            var park = context.Parkings.First(p => p.Id == parkings.Result[index].Id);
            return park.Occupied;
        }
    }
}
