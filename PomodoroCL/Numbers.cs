using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class is an ASCII art number generator
namespace PomodoroCL
{
    public class Numbers
    {
        string[] digits = { "  _____    /  _  |  /  / |  | |  |_/  /  0_____/  ",
                           "  ____     /_   |     |   |     |   |     |___1   ",
                           " _______  /_____  |  / _____/ / 2_____  |______/  ",
                           " _______  /_____  3   |   |    /______)           ",
                           "  ______   /   ^  |_/  4__|  /|____   |      |__| ",
                           " ._______  |   ___/  |____  5  ____/  / /______/  ",
                           "  _______  /  ____/ /   __  | |  6__| | |______/  ",
                           " ________ /____   7     /  /     /  /     /__/    ",
                           " _______  |   8   |  >     <  |   |   | |_______| ",
                           "  ______   /      | /   9   | |____   /    /___/  "};

        public string CreateNumber(int number)
        {
            // split the 2-digit int argument into 2 separate ints
            int firstDigit = number / 10;
            int secondDigit = number % 10;

            // output a string based on substrings of digits[]
            string outputNumber = " " + 
                digits[firstDigit].Substring(0, 10) + digits[secondDigit].Substring(0, 10) + "\n" +
                digits[firstDigit].Substring(9, 10) + digits[secondDigit].Substring(9, 10) + "\n" +
                digits[firstDigit].Substring(19, 10) + digits[secondDigit].Substring(19, 10) + "\n" +
                digits[firstDigit].Substring(29, 10) + digits[secondDigit].Substring(29, 10) + "\n" +
                digits[firstDigit].Substring(39, 10) + digits[secondDigit].Substring(39, 10) + "\n";

            return outputNumber;
        }
    }
}
