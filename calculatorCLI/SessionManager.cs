using System;


namespace calculatorCLI
{
    class SessionManager : IFileManager // exists to choose desired file management mode

    {
        private IFileManager mode;

        public SessionManager()
        {
            int Mode = 2; //app.Default.mode;
            switch (Mode)
            {
                case 1:
                    mode = new XMLDocument();
                    break;
                case 2:
                    mode = new LinqToXML();
                    break;
                case 3:
                    mode = new AutomaticSerialization();
                    break;

            }
        }
        public void SaveSession(string path, BuffCalculator calc)
        {
            try
            {
                mode.SaveSession(path, calc);
            }
            catch (Exception e)
            {
                Console.WriteLine("Whoops, something's not right! Issue: {0}", e.Message);
                Console.WriteLine(e.Data);
            }
        }

        
        
        public SerializableDictionary<int, double> LoadSession(string path)
        {
            try
            {
                return mode.LoadSession(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Whoops, something's not right! Issue: {0}", e.Message);
                Console.WriteLine(e.Data);
                return null;
            }
        }
    }
}
