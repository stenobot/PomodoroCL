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
        private int pomodoroLength = 25;

       // whether timer has been set at beginning of each pomodoro
        bool setTimer = true;

        // whether first clock tick has happened at beginning of each pomodoro
        bool firstClick = true;

        // whether first run has happened at beginning of task
        bool firstRun = true;

        public void SetupPomodoros()
        {
            // keep track of user's goal
            Console.WriteLine("\nWhat task do you want to complete?");
            currentTask = Console.ReadLine();

            // keep track of # of Pomodoros
            Console.WriteLine("\nHow many pomodoros will it take?");
            totalPomodoros = Console.ReadLine();

            // check if the value entered is numeric
            bool isNumeric = false;
            int n;
            isNumeric = int.TryParse(totalPomodoros, out n);

            while (isNumeric == false)
            {
                // if the value isn't numeric, ask again and check again 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nOops, I was expecting a number, \nbut you entered: " + totalPomodoros + "\nPlease try entering a number:\n");
                Console.ForegroundColor = ConsoleColor.White;
                totalPomodoros = Console.ReadLine();
                isNumeric = int.TryParse(totalPomodoros, out n);
            }

            // confirm, wait for user to press Enter
            Console.WriteLine("\nGot it. You want to");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(currentTask + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("in");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(totalPomodoros + " pomodoros\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to start ...\n");
            Console.ReadKey();

            // parse the string to an int
            totalPomodorosNumber = int.Parse(totalPomodoros);
        }

        // intro runs at the start of every pomodoro
        public void RunIntro()
        {
            Console.WriteLine("Three...");
            Thread.Sleep(1000);
            Console.WriteLine("Two...");
            Thread.Sleep(1000);
            Console.WriteLine("One...");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ___  __  _____  __ __   __  __  __ \n|   ||  ||  |  ||  || | |  ||  ||  ||\n|___||  ||  |  ||  ||  ||  ||__/|  ||\n|    |  ||  |  ||  ||  ||  || | |  ||\n|    |__||  |  ||__||_/ |__||  ||__|x\n");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }

        // reset to start a new pomodoro
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
                        string minuteS, secondS, minuteNum, secondNum;

                        if (minuteCount < 10)
                            minuteNum = "0{0}";
                        else
                            minuteNum = "{0}";

                        if (secondCount < 10)
                            secondNum = "0{1}";
                        else
                            secondNum = "{1}";

                        // display the minute and second count
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(minuteNum + ":" + secondNum,
                            minuteCount, secondCount);

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

                        // create variables to hold the actual hour, minute, and second
                        string currentHour, currentMinute, currentSecond;

                        // add a 0 if the hour, minute, or second is less than 10
                        if (e.Hour < 10)
                            currentHour = "0" + e.Hour.ToString();
                        else
                            currentHour = e.Hour.ToString();

                        if (e.Minute < 10)
                            currentMinute = "0" + e.Minute.ToString();
                        else
                            currentMinute = e.Minute.ToString();

                        if (e.Second < 10)
                            currentSecond = "0" + e.Second.ToString();
                        else
                            currentSecond = e.Second.ToString();

                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        // display minute count using ASCII art
                        Console.WriteLine("\n" + numbers.CreateNumber(minuteCount) + "\n");

                        // display minute count and actual time
                        if (minuteCount == 1)
                        {
                            Console.WriteLine("One minute has elapsed\nTime: {0}:{1}:{2}\n", 
                                currentHour,
                                currentMinute,
                                currentSecond);
                        }
                        else
                        {
                            Console.WriteLine(minuteCount + " minutes have elapsed\nTime: {0}:{1}:{2}\n",
                                currentHour,
                                currentMinute,
                                currentSecond);
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
                        Console.WriteLine("\nYou're done! That was the last pomorodo for:\n");
                        Console.WriteLine(currentTask + "\n");
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
