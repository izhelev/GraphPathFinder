using System.IO;
using System.Xml.Serialization;
using GraphPathFinder.Models;

namespace GraphPathFinder.Utilities
{
    public class NodeXmlDeserializer
    {
        private readonly XmlSerializer _serializer;

        public NodeXmlDeserializer()
        {
            _serializer = new XmlSerializer(typeof (Node));
        }


        public Node FromXml(string testXml)
        {
            using (TextReader textReader = new StringReader(testXml))
            {
                return (Node) _serializer.Deserialize(textReader);
            }
        }
    }
}