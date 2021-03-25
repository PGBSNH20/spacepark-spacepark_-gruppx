﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Parking : IParking
    {
        public int Id { get; set; }
        public int Fee { get; set; }
        public decimal MaxLength { get; set; }
        public bool Occupied { get; set; }
        [MaxLength(50)]
        public string ParkedBy { get; set; }
        [MaxLength(50)]
        public string User { get; set; }

        public void Park(IShipResult ship)
        {
            StandardMessages.LoadingMessage();
            var parkings = ParkingLots();
            var array = ArrayBuilder.ParkArray(parkings);
            Console.Clear();
            var selectedOption = Menu.ShowMenu($"You have selected to park {ship.Name}. Choose parking spot", array);

            Finish(parkings, selectedOption, ship);
        }

        private static bool SizeTooBig(Task<List<IParking>> parkings, int index, IShipResult ship)
        {
            return ship.Length > parkings.Result[index].MaxLength;
        }

        public async Task<List<IParking>> ParkingLots()
        {
            await using var context = new SpaceContext();
            var parkings = context.Parkings.OrderBy(i => i.Id).ToList();
            var list = parkings.ToList<IParking>();
            return list;
        }

        private static void Park(Task<List<IParking>> parkings, int index, IShipResult ship)
        {
            Console.Clear();
            using var context = new SpaceContext();
            Console.WriteLine("Parking..");
            var park = context.Parkings.First(p => p.Id == parkings.Result[index].Id);
            park.Occupied = true;
            park.User = ship.Name;
            park.ParkedBy = ship.Driver;
            context.SaveChangesAsync();
            Console.WriteLine("Parked");
            Console.ReadKey();
        }

        private static void Finish(Task<List<IParking>> parkings, int index, IShipResult ship)
        {
            if (SizeTooBig(parkings, index, ship))
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
                    Park(parkings, index, ship);
                }
            }
        }

        public void LeavePark()
        {
            IPerson person = new Person();
            IPayment payment = new Payment();
            string name = StandardMessages.NameReader();
            var r = person.GetAllPersons();
            StandardMessages.LoadingMessage();
            if (r.Result.Any(p => p.Name == name))
            {
                var parkings = ParkingLots();
                var array = ArrayBuilder.OnLeaveArray(parkings);
                Console.Clear();
                var selectedOption = Menu.ShowMenu($"Welcome {name}. What ship will you be leaving with today?\n", array);

                if (Occupation.ParkIsOccupied(parkings, selectedOption) == false)
                    StandardMessages.EmptyParkingLotMessage();
                else if(parkings.Result[selectedOption].ParkedBy != name)
                    StandardMessages.NotYourShipMessage();
                else
                {
                    payment.Pay(parkings, selectedOption, name);
                    Leave(parkings, selectedOption);
                }
            }
            else
            {
                StandardMessages.NotAllowedMessage();
            }
        }
        private static void Leave(Task<List<IParking>> parkings, int index)
        {
            using var context = new SpaceContext();
            Console.WriteLine("Leaving...");
            var ship = context.Parkings.First(s => s.Id == parkings.Result[index].Id);
            ship.Occupied = false;
            ship.User = null;
            ship.ParkedBy = null;
            context.SaveChangesAsync();
            Console.Clear();
            Console.WriteLine("Left.");
            Console.ReadKey();
        }
    }
}
