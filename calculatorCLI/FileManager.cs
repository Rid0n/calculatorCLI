using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorCLI
{
    interface FileManager
    {
        public void SaveSession(string path, BuffCalculator calculatorState);
        public Dictionary<int,double> LoadSession(string path);
    }
}
