using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using RestSharp;

namespace ClassLibrary.Api
{
    public class ShipApi
    {
        public async Task<Starship> GetStarships(string address) // Get all starships from API
        {
            var client = new RestClient("https://swapi.dev/api/");
            var request = new RestRequest("starships/" + address, DataFormat.Json);
            var response = await client.GetAsync<Starship>(request);
            return response;
        }
        private static async Task<Starship> CreateTask(string address) // Create tasks to run each page of API request async
        {
            var shipApi = new ShipApi();
            return await Task.Run(() => shipApi.GetStarships(address));
        }
        public Starship SelectShip() // Get the name from input and check if name exists in API request.
        {
            var personApi = new PersonApi();
            string name = StandardMessages.NameReader();
            var apiResult = personApi.GetAllPersons();
            Results user = apiResult.Result.FirstOrDefault(p => p.Name == name);

            if (user != null)
            {
                Console.Clear();

                List<string> shipsAddresses = user.Starships; // List the users starship URLs
                if (shipsAddresses.Count != 0)
                {
                    IEnumerable<string> addressNumber = GetShipNumber(shipsAddresses); // Extract the ship number from URL.
                    List<Starship> ownShips = new();
                    foreach (string s in addressNumber)
                    {
                        ownShips.Add(CreateTask(s).Result); // Run API request to get each ship and put in list
                    }
                    ownShips = RemoveParkedShips(ownShips); // Remove ships if they are already parked in the database.

                    if (ownShips.Count != 0)
                    {
                        var array = ArrayBuilder.ShipArray(ownShips); // Build an array of the ships from the list to present to the user in the menu.
                        var selectedOption = Menu.ShowMenu($"Welcome {name}. What ship will you be parking today?\n", array);

                        ownShips[selectedOption].Driver = name; // Assign the name to the driver property of the selected ship.
                        return ownShips[selectedOption];
                    }
                    StandardMessages.AllShipsParked(); // If all available ships for the user is already parked
                    return null;
                }
                StandardMessages.NoShipsAvailableMessage(); // If the user does not have any ships
                return null;
            }
            StandardMessages.NotAllowedMessage(); // If the user does not exist in the API
            return null;
        }
        private static List<Starship> RemoveParkedShips(List<Starship> ownShips) // Remove ships if they are already parked in the database.
        {
            using var context = new SpaceContext();
            var parkedShips = context.Parkings.Where(p => p.Occupied).ToList();
            for (int i = 0; i < parkedShips.Count; i++)
            {
                var name = parkedShips[i].ShipName;
                if (ownShips.Any(n => n.Name == name))
                {
                    var index = ownShips.First(s => s.Name == name);
                    ownShips.Remove(index);
                }
            }
            return ownShips;
        }
        public IEnumerable<string> GetShipNumber(IEnumerable<string> shipsAddresses) // Extract the ship number from URL.
        {
            return from s in shipsAddresses let index = s.TrimEnd('/').LastIndexOf('/') select s.Substring(index + 1);
        }
    }
}
