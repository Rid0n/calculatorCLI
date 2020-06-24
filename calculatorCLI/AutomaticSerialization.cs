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
            //DataContractSerializer dataSerializer = new DataContractSerializer(typeof(BuffCalculator));
            //using (StreamReader sr = new StreamReader(path))
            //{
            //    using (var reader = XmlDictionaryReader.Create(sr))
            //    {
            //        BuffCalculator calc = (BuffCalculator)dataSerializer.ReadObject(reader);
            //        Console.WriteLine("Session loaded!");
            //        return calc.dict;
            //    }
            //}
            XmlSerializer ser = new XmlSerializer(typeof(BuffCalculator));
            using (FileStream stream = new FileStream(path, FileMode.Open))
            using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
            {
                //DataContractSerializer dataSerializer = new DataContractSerializer(typeof(BuffCalculator));
                //BuffCalculator calc = (BuffCalculator)dataSerializer.ReadObject(reader);

                BuffCalculator calc = (BuffCalculator)ser.Deserialize(reader);
                Console.WriteLine("Session loaded!");
                return calc.dict;
            }

        }
        public void SaveSession(string path, BuffCalculator calculatorState)
        {
            ////DataContractSerializer dataSerializer = new DataContractSerializer(typeof(BuffCalculator));
            //var ns = new XmlSerializerNamespaces();
            //ns.Add("", "");
            //XmlSerializer ser = new XmlSerializer(typeof(BuffCalculator));
            //StringBuilder sb = new StringBuilder();

            //using (var writer = new StringWriter(sb))
            //using (var xmlwriter = XmlWriter.Create(writer))
            //{
            //    ser.Serialize(xmlwriter, calculatorState, ns);

            //    //dataSerializer.WriteObject(xmlwriter, calculatorState);
            //    Console.WriteLine("Session saved!");
            //}
            //using (var stream = new StreamWriter(path))
            //{
            //    stream.Write(sb);
            //}
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer ser = new XmlSerializer(typeof(BuffCalculator));
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream))
                {
                    ser.Serialize(writer, calculatorState,ns);
                    //dataSerializer.WriteObject(stream, calculatorState);
                    Console.WriteLine("Session saved!");
                }

            }
        }
    }
}

