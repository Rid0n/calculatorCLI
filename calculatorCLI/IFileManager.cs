using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorCLI
{
    interface IFileManager
    {
        public void SaveSession(string path, BuffCalculator calculatorState);
        public SerializableDictionary<int, double> LoadSession(string path);
    }
}
