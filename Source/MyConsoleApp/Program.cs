using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace MyConsoleApp
{
        
    class Program
    {
        static void EnterName() 
        {
            Console.Write("Enter your name: ");
            string userInput = Console.ReadLine();
            // ...
        }

        static async Task Main(string[] args)
        {
            // Entity stuff:
            MyContext context = new MyContext();
            var peo = new People()
            {
                Name = "hej",
            };
            context.People.Add(peo);
            context.SaveChanges();
            Console.WriteLine("User saved to database");
            //


            StarshipResponse starshipResponse = await new StarshipResponse().GetShips();
            PeopleResponse peopleResponse = await new PeopleResponse().GetPeople();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("All spaceships:");
            Console.ResetColor();
            Console.WriteLine();
            foreach (var p in starshipResponse.Results)
            {
                Console.WriteLine(p.Name);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("All people:");
            Console.ResetColor();
            Console.WriteLine();
            foreach (var item in peopleResponse.Results)
            {
                Console.WriteLine(item.Name);
            }

            Console.ReadKey();
        }
    }

    
}
