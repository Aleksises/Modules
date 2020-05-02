using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Task1.Models;

namespace Task1
{
    public class Program
    {
        public static void Main()
        {
            var serializer = new XmlSerializer(typeof(Catalog));

            using var fileStream = File.OpenRead("books.xml");
            var catalog = (Catalog)serializer.Deserialize(fileStream);

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, Catalog.XmlNamespace);

            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true
            };
            using var xmlWriter = XmlWriter.Create("books1.xml", settings);
            serializer.Serialize(xmlWriter, catalog, namespaces);
        }
    }
}
