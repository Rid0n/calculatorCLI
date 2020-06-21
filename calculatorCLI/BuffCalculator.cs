using System;
using System.Collections.Generic;
using System.Text;



namespace calculatorCLI
{
    
    
    class BuffCalculator
    {
        private double operand;
        private Dictionary<int, double> dict = new Dictionary<int, double>();
        private bool flag = true;
        private bool SessionActive = true;
        private double n;
        UtilityFunctions Utility = new UtilityFunctions();
        private void DisplayTips()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("when first symbol on line is ‘>’ – enter operand(number)");
            Console.WriteLine("when first symbol on line is ‘@’ – enter operation");
            Console.WriteLine("operation is one of ‘+’, ‘-‘, ‘/’, ‘*’ or");
            Console.WriteLine("‘#’ followed with number of evaluation step ‘q’ to return to menu");
        }
        private void DisplayMenu()
        {
            Console.WriteLine("Menu \n 1. Begin/Resume calculation \n 2. Show calculation history \n 3. Display n-th calculation \n 4. Start calculator from n-th step \n 5. Save current session(WIP) \n 6. Load another session (WIP) \n 7. Clear Session \n 8. Exit");
        }
        private void DisplayHistory()
        {
            Console.WriteLine("Log of operations");
            if (dict.Count == 0) Console.WriteLine("Empty!");
            else
            {
                foreach (KeyValuePair<int, double> item in dict)
                {
                    Console.WriteLine("Result at step {0} : {1}", item.Key, item.Value);
                }
            }
        }
        private void ShowDesiredCalculation()
        {
            Console.WriteLine("Enter the step");
            int step = Utility.ReadIntInput(); // define the calculation
            if (dict.Count >= step) Console.WriteLine("Evaluation at step {0} yielded {1}", step, dict[step]); // check errors
            else Console.WriteLine("no such step!");
        }

        public BuffCalculator()
        {
            
            DisplayMenu();
            Console.WriteLine(">:");
            while (SessionActive)
            {
                switch (Utility.ReadIntInput())
                {
                    case 1:
                        DisplayTips();

                        if (dict.Count == 0)
                        {
                            Console.WriteLine(">:");
                            n = Utility.ReadDoubleInput();
                            dict.Add(1, n);
                            Calculator();
                        }
                        else
                        {
                            Calculator();
                        }
                        break;
                    case 2:
                        DisplayHistory();
                        break;
                    case 3:
                        ShowDesiredCalculation();
                        break;
                    case 4:

                        DisplayTips();
                        Console.WriteLine("Enter evaluation step");
                        int value = Utility.ReadIntInput();
                        dict.Add(dict.Count + 1, dict[value]);
                        Calculator();
                        break;
                    case 5:
                        Console.WriteLine("WIP");
                        break;
                    case 6:
                        Console.WriteLine("WIP");
                        break;
                    case 7:
                        if (Utility.YorN() == "y") dict.Clear();
                        break;
                    case 8:
                        SessionActive = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        DisplayMenu();
                        break;
                }
            }
            
        }
        private void Calculator()
        {
            int step = dict.Count;
            flag = true;
            n = dict[step];
            Console.WriteLine("#{0}:{1}", step, n);

            while (flag)
            {
                Console.WriteLine("@:");
                string line = Console.ReadLine();
                if (!string.IsNullOrEmpty(line) && (line == "/" || line == "+" || line == "q" || line == "*" || line == "-" || line[0] == '#'))
                {
                    Console.WriteLine(">:");
                    switch (line)
                    {
                        case "+":
                            operand = Utility.ReadDoubleInput();
                            n += operand;
                            goto default;
                        case "-":
                            operand = Utility.ReadDoubleInput();
                            n -= operand;
                            goto default;
                        case "/":
                            operand = Utility.ReadDoubleInput();
                            n /= operand;
                            goto default;
                        case "*":
                            operand = Utility.ReadDoubleInput();
                            n *= operand;
                            goto default;
                        case "q":
                            flag = false;
                            break;
                        default:
                            if (line[0] == '#')
                            {
                                bool r = int.TryParse(line.Remove(0, 1), out int key);
                                if (key <= dict.Count && r) n = dict[key];

                            }
                            step++;
                            Console.WriteLine("#{0}:{1}", step, n);
                            dict.Add(step, n);
                            break;
                    }
                }
            }
            foreach (var item in dict)
            {
                Console.WriteLine("{0} - {1}", item.Key, item.Value);
            }
        }

    }
}
