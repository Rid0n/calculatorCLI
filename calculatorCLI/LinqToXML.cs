using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace calculatorCLI
{
    class LinqToXML : FileManager
    {
        public Dictionary<int,double> LoadSession(string path)
        {
            XElement xml = XElement.Load("ha.xml");
            Dictionary<int, double> dict = new Dictionary<int, double>();
            dict = xml.Descendants("Calculation").Descendants("KeyValuePairOfintdouble").ToDictionary(p => int.Parse(p.Element("Key").Value), p => double.Parse(p.Element("Value").Value));
            Console.WriteLine("Session Loaded!");
            return dict;
        }
        public void SaveSession(string path,BuffCalculator calculatorState)
        {
            Dictionary<int, double> dict = calculatorState.dict;
            
            XDocument xml = new XDocument();
            XElement root = new XElement("CalculatorSession");
            XElement calc = new XElement("Calculation");
            //XElement root = new XElement("CalculatorSession", new XElement("Calculation", new XElement("KeyValuePairOfintdouble",
            //    from keyValue in dict select new XElement(keyValue.Key.ToString(), keyValue.Value)
            //)));
            root.Add(calc);
            foreach (var item in dict) // add new keyvalue pairs
            {
                XElement pair = new XElement("KeyValuePairOfintdouble");
                pair.Add(new XElement("Key", item.Key));
                pair.Add(new XElement("Value", item.Value));
                calc.Add(pair);
            }

            xml.Add(root);
            xml.Save(path);
            Console.WriteLine("Session Saved!");
        }
    }
}
