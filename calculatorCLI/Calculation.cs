using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorCLI
{
    class Calculation
    {
        public double result { get; private set; } // dictionary?
        public string expression { get; private set; } // not string
        public int step { get; private set; }
    }
    // now i might wanna add expressions (the one before ) | for that we need to save things
    // WORK IN PROGRESS
}
