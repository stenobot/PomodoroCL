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
            Console.WindowWidth = 45;

            // create instances of Clock and Pomodoro classes
            var theClock = new Clock();
            var pomodoro = new Pomodoro();
            
            // call subscribe method
            pomodoro.Subscribe(theClock);

            // app title
            Console.WriteLine("PomodoroCL - 2015\n");

            // run the Clock
            theClock.RunClock(true);


        }
    }
}
