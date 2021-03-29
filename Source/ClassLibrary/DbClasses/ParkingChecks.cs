using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary
{
    public class ParkingChecks
    {
        public async Task<List<Parking>> ParkingLots() // Get all parking lots
        {
            await using var context = new SpaceContext();
            List<Parking> list = context.Parkings.OrderBy(i => i.Id).ToList();
            return list;
        }
        public bool SizeTooBig(Task<List<Parking>> parkings, int index, Starship ship)
        {
            return ship.Length > parkings.Result[index].MaxLength;
        }
    }
}
