using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void Subscribe(Clock theClock)
        {
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

                        if (minuteCount > 1)
                            minuteS = "s";
                        else
                            minuteS = "";

                        if (secondCount > 1)
                            secondS = "s";
                        else
                            secondS = "";

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("{0} minute" + minuteS + ", {1} second" + secondS, minuteCount, secondCount);

                        // check second count against the clock
                        // this will always be true when timer starts
                        if (e.Second != 0) 
                            secondCount = secondCount + (e.Second - previousSecond);
                        else
                            secondCount++;
                    }
                    else // the minute marker has been reached
                    {
                        // increment the minute count
                        minuteCount++;

                        // let user know a minute has elapsed
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        if (minuteCount == 1)
                        {
                            Console.WriteLine("One minute has elapsed - {0}:{1}:{2}", 
                                e.Hour.ToString(),
                                e.Minute.ToString(),
                                e.Second.ToString());
                        }
                        else
                        {
                            Console.WriteLine(minuteCount + " minutes have elapsed- {0}:{1}:{2}",
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
                    // let the user know
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You're done. Take a break!");
                }
                previousSecond = e.Second;
            };
        }
    }
}
