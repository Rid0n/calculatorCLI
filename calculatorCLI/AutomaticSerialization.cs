using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System;
namespace calculatorCLI
{
    class AutomaticSerialization : FileManager
    {
        public Dictionary<int,double> LoadSession(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream,new XmlDictionaryReaderQuotas()))
            {
                DataContractSerializer dataSerializer = new DataContractSerializer(typeof(BuffCalculator));
                BuffCalculator calc = (BuffCalculator)dataSerializer.ReadObject(reader);
                Console.WriteLine("Session loaded!");
                return calc.dict;
            }
        }
        public void SaveSession(string path, BuffCalculator calculatorState)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream))
                {
                    DataContractSerializer dataSerializer = new DataContractSerializer(typeof(BuffCalculator)); // change
                    dataSerializer.WriteObject(stream, calculatorState);
                    Console.WriteLine("Session saved!");
                }
            }
        }


    }
}
