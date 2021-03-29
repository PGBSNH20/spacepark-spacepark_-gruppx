using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassLibrary.Models;
using RestSharp;

namespace ClassLibrary.Api
{
    public class PersonApi
    {
        private static async Task<Person> GetPersonPage(int page) // Get one page from API
        {
            var client = new RestClient("https://swapi.dev/api/");
            var request = new RestRequest($"people/?page={page}", DataFormat.Json);
            var response = await client.GetAsync<Person>(request);
            return response;
        }
        private static Task<Person> CreateTask(int i) // Create task to run several requests async
        {
            return Task.Run(() => GetPersonPage(i));
        }
        public async Task<List<Results>> GetAllPersons() // Return list of all persons in API
        {
            var p = await GetPersonPage(1);
            int numberOfPersons = int.Parse(p.Count);
            int personsPerOnePage = p.Results.Count;
            int numberOfPages = (int)Math.Ceiling(1.0 * numberOfPersons / personsPerOnePage); // Use the number of persons and persons per page to calculate number of pages

            var tasks = new List<Task<Person>>();
            for (int i = 1; i < numberOfPages + 1; i++) // For each page create a task to run all pages at the same time.
            {
                tasks.Add(CreateTask(i));
            }

            List<Results> temp = new();

            for (int i = 0; i < numberOfPages; i++)
            {
                temp.AddRange(tasks[i].Result.Results); // Add each page result to list
            }
            return temp;
        }
    }
}
