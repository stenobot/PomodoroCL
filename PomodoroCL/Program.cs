using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PomodoroCL
{
    class Program
    {
        static void Main(string[] args)
        {
            // keep window width small
            Console.WindowWidth = 50;

            // create instances of Clock and Pomodoro classes
            var theClock = new Clock();
            var pomodoro = new Pomodoro();
            
            // call subscribe method
            pomodoro.Subscribe(theClock);

            // app title
            Console.WriteLine("PomodoroCL - 2015\n");

            // keep track of user's goal
            Console.WriteLine("\nWhat's your goal for this Pomodoro:");
            string currentPomodoro = Console.ReadLine();

            // keep track of # of Pomodoros
            Console.WriteLine("\nHow many Pomodoros will it take?");
            string totalPomodoros = Console.ReadLine();

            // wait for user to press Enter
            Console.WriteLine("\nGot it. Press any key to start this Pomodoro...\n");
            Console.ReadKey();

            // run intro
            pomodoro.RunIntro();

            // run the Clock
            theClock.RunClock(true);
        }
    }
}
