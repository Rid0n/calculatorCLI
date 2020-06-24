using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace calculatorCLI
{
    [XmlRoot("LogOfOperations")]
    //[DataContract(Namespace="",Name ="LogOfOperations")]
    public class BuffCalculator
    {
        
        //[DataMember(Name ="Operations")]
        [XmlElement("Entries")]
        public SerializableDictionary<int,double> dict = new SerializableDictionary<int, double>();
        private bool SessionActive = true;
        private SessionManager manager = new SessionManager();
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
            Console.WriteLine("Menu \n 1. Begin/Resume calculation \n 2. Show calculation history \n 3. Display n-th calculation \n 4. Start calculator from n-th step \n 5. Save current session \n 6. Load another session \n 7. Clear Session \n 8. Exit");
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
        public void StartSession()
        {
            DisplayMenu();
            Console.WriteLine(">:");
            while (SessionActive)
            {
                switch (Utility.ReadIntInput())
                {
                    case 1:
                        DisplayTips();

                        if (dict == null || dict.Count == 0)
                        {
                            Console.WriteLine(">:");
                            dict.Add(1, Utility.ReadDoubleInput());
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
                        Console.WriteLine("Enter filepath. (ex. foo.xml)");
                        manager.SaveSession(Console.ReadLine(), this);

                        break;
                    case 6:
                        Console.WriteLine("Enter filepath. (ex. foo.xml)");
                        dict = manager.LoadSession(Console.ReadLine());
                        break;
                    case 7:
                        if (Utility.YorN()) dict.Clear();
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
            bool flag = true;
            double operand;
            double n;
            int step = dict.Count;
            n = dict[step];
            Console.WriteLine("#{0}:{1}", step, n);

            while (flag)
            {
                Console.WriteLine("@:");
                string line = Console.ReadLine();
                if (line == "/" || line == "+" || line == "q" || line == "*" || line == "-" || line[0] == '#')
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
            
        }

    }
}
