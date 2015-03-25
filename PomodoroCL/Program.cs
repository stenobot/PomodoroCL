using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PomodoroCL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 45;
            var theClock = new Clock();

            var pomodoro = new Pomodoro();
            
            pomodoro.Subscribe(theClock);

            Console.WriteLine("PomodoroCL - 2015\n");

            Console.WriteLine("Press Enter to start Pomodoro...\n");
            Console.ReadKey();

            // intro
            Thread.Sleep(1000);
            Console.WriteLine("Three...");
            Thread.Sleep(1000);
            Console.WriteLine("Two...");
            Thread.Sleep(1000);
            Console.WriteLine("One...");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("_____ ____ _______ ____ __   ____ ___  ____\n|   | |  | |  |  | |  | | |  |  | |  | |  | |\n|___| |  | |  |  | |  | |  | |  | |__/ |  | |\n|     |  | |  |  | |  | |  | |  | | |  |  | |\n|     |__| |  |  | |__| |_/  |__| |  | |__| .\n");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);

            // run Clock
            theClock.RunClock();
        }
    }
}
