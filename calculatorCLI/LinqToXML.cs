using System;
using System.Collections.Generic;
using System.Xml.Linq;
namespace calculatorCLI
{
    class LinqToXML : IFileManager
    {
        public SerializableDictionary<int, double> LoadSession(string path)
        {
            XElement xml = XElement.Load(path);
            SerializableDictionary<int, double> dict = new SerializableDictionary<int, double>();
            foreach (var item in xml.Descendants("Entries").Descendants("Item"))
            {
                int key = int.Parse(item.Element("Key").Value);
                double value = double.Parse(item.Element("Value").Value);
                dict.Add(key, value);
            }
            Console.WriteLine("Session Loaded!");
            return dict;
        }
        public void SaveSession(string path,BuffCalculator calculatorState)
        {
            Dictionary<int, double> dict = calculatorState.dict;
            
            XDocument xml = new XDocument();
            XNamespace ns = "http://calculatorCLI";
            XElement root = new XElement(ns + "LogOfOperations");
            XElement calc = new XElement(ns + "Entries");

            root.Add(calc);
            foreach (var pair in dict) // add new keyvalue pairs
            {
                XElement Item = new XElement(ns + "Item");
                Item.Add(new XElement(ns + "Key", new XElement(ns + "int",pair.Key)));
                Item.Add(new XElement(ns + "Value", new XElement(ns + "double",pair.Value)));
                calc.Add(Item);
            }

            xml.Add(root);
            xml.Save(path);
            Console.WriteLine("Session Saved!");
        }
    }
}
