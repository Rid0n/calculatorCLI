using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System;
using System.Text;
using System.Xml.Serialization;
using Polenter.Serialization;

namespace calculatorCLI
{
    class AutomaticSerialization : IFileManager
    {
        public SerializableDictionary<int, double> LoadSession(string path)
        {

            XmlSerializer ser = new XmlSerializer(typeof(BuffCalculator));
            using (FileStream stream = new FileStream(path, FileMode.Open))
            using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
            {

                BuffCalculator calc = (BuffCalculator)ser.Deserialize(reader);
                Console.WriteLine("Session loaded!");
                return calc.dict;
            }

        }
        public void SaveSession(string path, BuffCalculator calculatorState)
        {

            var ns = new XmlSerializerNamespaces();
            XmlSerializer ser = new XmlSerializer(typeof(BuffCalculator),"http://calculatorCLI");
            ns.Add(string.Empty, "http://calculatorCLI");
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream))
                {

                    ser.Serialize(writer, calculatorState,ns);
                    Console.WriteLine("Session saved!");
                }

            }
        }
    }
}

