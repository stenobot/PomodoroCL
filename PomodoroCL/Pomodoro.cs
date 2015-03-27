using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PomodoroCL
{
    public class Pomodoro
    {
        // seconds elapsed
        int secondCount;

        // minutes elapsed
        int minuteCount;

        // initialize current pomodoro
        int pomodoroCount;

        // the previous second
        // to keep our count in sync with the clock
        int previousSecond;

        // user's task
        string currentTask;

        // how many pomodoros a task will take
        // string version that user will enter...
        string totalPomodoros;
        // ...parsed into int
        int totalPomodorosNumber;

        // length of a pomodoro in minutes
        private int pomodoroLength = 2;

       // whether timer has been set at beginning of each pomodoro
        bool setTimer = true;

        // whether first clock tick has happened at beginning of each pomodoro
        bool firstClick = true;

        // whether first run has happened at beginning of task
        bool firstRun = true;

        public void SetupPomodoros()
        {
            // keep track of user's goal
            Console.WriteLine("\nWhat task to you want to complete?");
            currentTask = Console.ReadLine();

            // keep track of # of Pomodoros
            Console.WriteLine("\nHow many pomodoros will it take?");
            totalPomodoros = Console.ReadLine();

            // confirm, wait for user to press Enter
            Console.WriteLine("\nOkey dokey. You want to");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(currentTask);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("in");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(totalPomodoros + " pomodoros");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to start ...\n");
            Console.ReadKey();

            // parse the string to an int
            totalPomodorosNumber = int.Parse(totalPomodoros);
        }

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

        public void ResetPomodoro()
        {
            secondCount = 0;
            minuteCount = 0;
            previousSecond = 0;
            setTimer = true;
            firstClick = true;
        }

        public void Subscribe(Clock theClock)
        {
            // create instance of ASCII art number generator
            var numbers = new Numbers();

            theClock.TimeChanged +=
            (sender, e) =>
            {
                if (firstRun)
                {
                    SetupPomodoros();
                    firstRun = false;
                }

                if (firstClick)
                {
                    RunIntro();
                    // don't continue on first click of the clock 
                    // so we can be sure to start clock at the beginning of a full second
                    firstClick = false;
                    return;
                }

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
                    // increment the pomodoro count
                    pomodoroCount++;

                    Console.ForegroundColor = ConsoleColor.Green;

                    if (pomodoroCount == totalPomodorosNumber)
                    {
                        //all done, stop everything
                        Console.WriteLine("You're done! That was the last pomorodo for:\n");
                        Console.WriteLine(currentTask);
                        Console.WriteLine("Press any key to start a new task...");
                        ResetPomodoro();
                        firstRun = true;
                        pomodoroCount = 0;
                        Console.ReadKey();
                    }
                    else
                    {
                        // take a fiver
                        Console.WriteLine("Pomodoro " + pomodoroCount + " of " + totalPomodorosNumber + " complete. Time for a break.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press any key to start the next pomodoro for:");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(currentTask + "\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        ResetPomodoro();
                    }    
                }

                // lastly, update previous second variable
                previousSecond = e.Second;
            };
        }
    }
}
