using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary
{
    public class PaymentActions
    {
        public void Pay(Task<List<Parking>> parkings, int index, string name)
        {
            Console.Clear();
            using var context = new SpaceContext();
            Console.WriteLine("Using swish to pay...");
            Payment pay = new Payment();
            pay.Amount = parkings.Result[index].Fee;
            pay.User = name;
            pay.PayDate = DateTime.Now;
            context.Add(pay);
            context.SaveChanges();
            Console.WriteLine("Payment successful");
            Console.WriteLine($"Payment By: {pay.User}. Amount: {pay.Amount} credits. Payment Date: {pay.PayDate}");
            Console.ReadKey();
        }

        public void Receipts()
        {
            using var context = new SpaceContext();
            string name = StandardMessages.NameReader();
            var payments = context.Payments.Where(p => p.User == name).ToList();
            if (!payments.Any())
            {
                Console.WriteLine("No receipts found.");
            }
            else
            {
                foreach (var r in payments)
                {
                    Console.WriteLine($"Payment By: {r.User}. Amount: {r.Amount} credits. Payment Date: {r.PayDate}");
                }
            }
            Console.ReadKey();
        }
    }
}
