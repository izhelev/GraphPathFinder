using System.Collections.Generic;
using System.Xml.Serialization;

namespace GraphPathFinder.Models
{
    [XmlRoot(ElementName="node")]
    public class Node
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("label")]
        public string Label { get; set; }
        [XmlArray("adjacentNodes")]
        [XmlArrayItem("id", typeof(int)) ]
        public List<int> AdjacentNodesIds { get; set; }
    }
}