using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class StandardMessages
    {
        public static void StartMessage()
        {
            Console.Clear();
            Console.WriteLine(@" ____  ____   _    ____ _____   ____   _    ____  _  __
/ ___||  _ \ / \  / ___| ____| |  _ \ / \  |  _ \| |/ /
\___ \| |_) / _ \| |   |  _|   | |_) / _ \ | |_) | ' / 
 ___) |  __/ ___ | |___| |___  |  __/ ___ \|  _ <| . \ 
|____/|_| /_/   \_\____|_____| |_| /_/   \_|_| \_|_|\_\");

            Console.WriteLine("");
        }

        public static void NotAllowedMessage()
        {
            Console.Clear();
            Console.WriteLine("Not Allowed. Press any key.");
            Console.ReadKey();
        }
        public static void FullParkMessage()
        {
            Console.Clear();
            Console.WriteLine("ParkingModel is full. Go away. Press any key.");
            Console.ReadKey();
        }

        public static void EmptyParkingLotMessage()
        {
            Console.Clear();
            Console.WriteLine("You cannot leave with nothing. Restart and try again. Press any key.");
            Console.ReadKey();
        }

        public static void LoadingMessage()
        {
            Console.Clear();
            Console.WriteLine("Loading...");
        }

        public static void NotYourShipMessage()
        {
            Console.Clear();
            Console.Write("Not your ship! Press any key.");
            Console.ReadKey();
        }

        public static string NameReader()
        {
            Console.Clear();
            Console.WriteLine("Enter name: ");
            return Console.ReadLine();
        }

        public static void AllShipsParked()
        {
            Console.Clear();
            Console.WriteLine("Sorry, all your ships are already parked.");
            Console.ReadKey();
        }

        public static void NoShipsAvailableMessage()
        {
            Console.Clear();
            Console.WriteLine("Sorry, you don't have any ships.");
            Console.ReadKey();
        }
    }
}
