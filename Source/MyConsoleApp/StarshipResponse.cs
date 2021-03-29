using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp
{
    internal class StarshipResponse : IStarshipResponse
    {

        public List<Starship> Results { get; set; }

        public StarshipResponse()
        {
            Results = new List<Starship>();
        }


        public async Task<StarshipResponse> GetShips()
        {
            bool finnishedList = true;
            int pageCount = 1;

            while (finnishedList)
            {
                var client = new RestClient("https://swapi.dev/api/");
                var request = new RestRequest("starships/?page=" + pageCount, DataFormat.Json);
                var peopleResponse = await client.GetAsync<StarshipResponse>(request);
                pageCount++;

                if (peopleResponse.Results.Count != 0)
                {
                    foreach (var s in peopleResponse.Results)
                    {
                        Results.Add(s);
                    }
                }
                else
                {
                    finnishedList = false;
                }
            }
            return this;
        }
    }
}
