using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorCLI
{
    class UtilityFunctions
    {
        public int ReadIntInput()
        {
            bool s = Int32.TryParse(Console.ReadLine(), out int input);
            while (!s)
            {
                Console.WriteLine("Invalid Format! Try again:");
                s = Int32.TryParse(Console.ReadLine(), out input);
            }
            return input;
        }
        public double ReadDoubleInput()
        {
            bool s = Double.TryParse(Console.ReadLine(), out double input);
            while (!s)
            {
                Console.WriteLine("Invalid Format! Try again:");
                s = Double.TryParse(Console.ReadLine(), out input);
            }
            return input;
        }
        public string YorN()
        {
            Console.WriteLine("are you sure? y\\n");
            string response = Console.ReadLine();
            bool exp = response == "y" || response == "n";
            while (!exp)
            {
                Console.WriteLine("Try y\\n");
                response = Console.ReadLine();
                exp = response == "y" || response == "n";
            }
            return response;
        }
    }
}
