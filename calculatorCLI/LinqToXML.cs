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
            XElement root = new XElement("LogOfOperations");
            XElement calc = new XElement("Entries");

            root.Add(calc);
            foreach (var pair in dict) // add new keyvalue pairs
            {
                XElement Item = new XElement("Item");
                Item.Add(new XElement("Key", new XElement("int",pair.Key)));
                Item.Add(new XElement("Value", new XElement("double",pair.Value)));
                calc.Add(Item);
            }

            xml.Add(root);
            xml.Save(path);
            Console.WriteLine("Session Saved!");
        }
    }
}
