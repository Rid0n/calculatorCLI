using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace calculatorCLI
{
    class XMLDocument: FileManager
    {
        public Dictionary<int, double> LoadSession(string path)
        {
            XmlDocument xml = new XmlDocument();
            Dictionary<int, double> dict = new Dictionary<int, double>();
            int key;
            double value;
            xml.Load(path);
            if (xml.FirstChild.HasChildNodes)
            {
                foreach (XmlNode item in xml.DocumentElement)
                {
                    foreach (XmlNode child in item.ChildNodes)
                    {
                        key = int.Parse(child.ChildNodes[0].InnerText);
                        value = double.Parse(child.ChildNodes[1].InnerText);
                        dict.Add(key, value);
                    }

                }
            }
            Console.WriteLine("Session Loaded!");
            return dict;
        }
        public void SaveSession(string path,BuffCalculator calculatorState)
        {
            XmlDocument xml = new XmlDocument();
            Dictionary<int, double> dict = calculatorState.dict;
            XmlElement root = xml.CreateElement("CalculatorSession");
            xml.AppendChild(root);
            XmlElement calc = xml.CreateElement("Calculation");
            root.AppendChild(calc);
            foreach (KeyValuePair<int, double> item in dict)
            {
                XmlElement keyValuePair = xml.CreateElement("a:KeyValueOfintdouble");
                XmlElement keyElement = xml.CreateElement("a:Key");
                XmlElement valueElement = xml.CreateElement("a:Value");
                XmlText key = xml.CreateTextNode(item.Key.ToString());
                XmlText value = xml.CreateTextNode(item.Value.ToString());
                keyElement.AppendChild(key);
                valueElement.AppendChild(value);

                keyValuePair.AppendChild(keyElement);
                keyValuePair.AppendChild(valueElement);
                calc.AppendChild(keyValuePair);
            }
            xml.Save(path);
            Console.WriteLine("Session Saved!");
        }
    }
}
