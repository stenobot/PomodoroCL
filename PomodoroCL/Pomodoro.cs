using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PomodoroCL
{
    public class Pomodoro
    {
        // keep track of seconds elapsed
        int secondCount;

        // keep track of minutes elapsed
        int minuteCount;

        // remember the previous second
        // to keep our count in sync with the clock
        int previousSecond;

        // set the length of a pomodoro in minutes
        private int pomodoroLength = 25;

        // only set up the timer once per session
        bool setTimer = true;

        public void RunIntro()
        {
            Console.WriteLine("Three...");
            Thread.Sleep(1000);
            Console.WriteLine("Two...");
            Thread.Sleep(1000);
            Console.WriteLine("One...");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ___   __   _____   __  __    __   _    __ \n|   | |  | |  |  | |  | | |  |  | |  | |  | |\n|___| |  | |  |  | |  | |  | |  | |__/ |  | |\n|     |  | |  |  | |  | |  | |  | | |  |  | |\n|     |__| |  |  | |__| |_/  |__| |  | |__| x\n");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }

        public void Subscribe(Clock theClock)
        {
            // create instance of ASCII art number generator
            var numbers = new Numbers();

            theClock.TimeChanged +=
            (sender, e) =>
            {
                if (setTimer)
                {
                    // second count begins at 1
                    secondCount = 1;

                    // initialize our previous second
                    previousSecond = e.Second - 1;

                    // make sure this doesn't run again
                    setTimer = false;
                }

                if (minuteCount < pomodoroLength)
                {
                    if (secondCount <= 59) // still counting in the current minute
                    {
                        string minuteS, secondS;

                        if (minuteCount != 1)
                            minuteS = "s";
                        else
                            minuteS = "";

                        if (secondCount != 1)
                            secondS = "s";
                        else
                            secondS = "";

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("{0} minute" + minuteS + ", {1} second" + secondS, minuteCount, secondCount);

                        // check second count against the clock
                        if (e.Second != 0) 
                            secondCount = secondCount + (e.Second - previousSecond);
                        else
                            secondCount++;
                    }
                    else // the minute marker has been reached
                    {
                        // increment the minute count
                        minuteCount++;

                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        // display minute count using ASCII art
                        Console.WriteLine(numbers.CreateNumber(minuteCount));

                        // display minute count and actual time
                        if (minuteCount == 1)
                        {
                            Console.WriteLine("One minute has elapsed - {0}:{1}:{2}", 
                                e.Hour.ToString(),
                                e.Minute.ToString(),
                                e.Second.ToString());
                        }
                        else
                        {
                            Console.WriteLine(minuteCount + " minutes have elapsed - {0}:{1}:{2}",
                                e.Hour.ToString(),
                                e.Minute.ToString(),
                                e.Second.ToString());
                        }

                        // reset the second count
                        secondCount = 1;
                    }
                }
                else // the pomodoro length has been reached
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(numbers.CreateNumber(minuteCount));
                    Console.WriteLine("You're done. Take a break!");

                    // stop the clock
                    theClock.RunClock(false);
                }

                // lastly, update previous second variable
                previousSecond = e.Second;
            };
        }
    }
}
