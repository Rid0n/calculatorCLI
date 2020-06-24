using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace calculatorCLI
{
    class XMLDocument: IFileManager
    {
        public SerializableDictionary<int, double> LoadSession(string path)
        {
            XmlDocument xml = new XmlDocument();
            SerializableDictionary<int, double> dict = new SerializableDictionary<int, double>();
            int key;
            double value;
            xml.Load(path);

            foreach (XmlNode item in xml.ChildNodes.Item(1))
            {
                foreach (XmlNode child in item.ChildNodes)
                {

                    key = int.Parse(child.ChildNodes[0].InnerText);
                    value = double.Parse(child.ChildNodes[1].InnerText);
                    dict.Add(key, value);
                }

            }

            Console.WriteLine("Session Loaded!");
            return dict;
        }
        public void SaveSession(string path,BuffCalculator calculatorState)
        {
            XmlDocument xml = new XmlDocument();
            Dictionary<int, double> dict = calculatorState.dict;
            XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "utf-8",null);
            xml.AppendChild(dec);

            XmlElement root = xml.CreateElement("LogOfOperations");
            xml.AppendChild(root);
            xml.DocumentElement.SetAttribute("xmlns","http://calculatorCLI");
            XmlElement calc = xml.CreateElement("Entries");
            root.AppendChild(calc);
            foreach (KeyValuePair<int, double> pair in dict)
            {
                XmlElement Item = xml.CreateElement("Item");
                XmlElement keyElement = xml.CreateElement("Key");
                XmlElement valueElement = xml.CreateElement("Value");
                XmlElement keyValue = xml.CreateElement("int");
                XmlElement valueValue = xml.CreateElement("double");
                XmlText key = xml.CreateTextNode(pair.Key.ToString());
                XmlText value = xml.CreateTextNode(pair.Value.ToString());

                keyValue.AppendChild(key);
                valueValue.AppendChild(value);
                keyElement.AppendChild(keyValue);
                valueElement.AppendChild(valueValue);


                Item.AppendChild(keyElement);
                Item.AppendChild(valueElement);
                calc.AppendChild(Item);
            }
            xml.Save(path);
            Console.WriteLine("Session Saved!");
        }
    }
}
