using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Task1.Models
{
    [XmlRoot(ElementName = "catalog", Namespace = XmlNamespace)]
    public class Catalog
    {
        public const string XmlNamespace = "http://library.by/catalog";

        [XmlAttribute(AttributeName = "date", DataType = "date")]
        public DateTime Date { get; set; }

        [XmlElement("book")]
        public List<Book> Books { get; set; }
    }
}
