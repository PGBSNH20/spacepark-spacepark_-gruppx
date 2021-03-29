using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp
{

    internal class PeopleResponse : IPeopleResponse
    {
        public int Count { get; set; }

        public List<People> Results { get; set; }

        public PeopleResponse()
        {
            Results = new List<People>();
        }

        public async Task<PeopleResponse> GetPeople()
        {
            bool finnishedList = false;
            int pageCount = 1;

            while (finnishedList == false)
            {
                var client = new RestClient("https://swapi.dev/api/");
                var request = new RestRequest("people/?page=" + pageCount, DataFormat.Json);
                var peopleResponse = await client.GetAsync<PeopleResponse>(request);
                pageCount++;

                if (peopleResponse.Results.Count != 0)
                {
                    foreach (var p in peopleResponse.Results)
                    {
                        Results.Add(p);
                    }
                }
                else
                {
                    finnishedList = true;
                }
            }
            return this;
        }

    }
}
