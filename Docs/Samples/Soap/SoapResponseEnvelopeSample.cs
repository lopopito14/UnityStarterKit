namespace Samples
{
    using System.Xml.Serialization;
    using UnityStarterKit.Web.Soap;

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapResponseEnvelopeSample : ISoapRequestEnvelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public string Header { get; set; }
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public SoapResponseBodySample Body { get; set; }
        [XmlAttribute(AttributeName = "soapenv", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soapenv { get; set; }
        [XmlAttribute(AttributeName = "urn", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Urn { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapResponseBodySample
    {
        [XmlElement(ElementName = "SoapResponseSample", Namespace = "")]
        public SoapResponseSample SoapResponseSample { get; set; }
    }

    [XmlRoot(ElementName = "SoapResponseSample", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapResponseSample
    {
        [XmlAttribute(AttributeName = "payload", DataType = "string")]
        public string payload { get; set; }
    }
}