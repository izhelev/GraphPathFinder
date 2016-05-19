using System.Collections.Generic;
using GraphPathFinder.Utilities;
using NUnit.Framework;

namespace GraphPathFinder.UnitTests
{
    [TestFixture]
    public class NodeXmlDeserializerTestFixtures
    {
        [TestCase]
        public void GivenAValidXmlFileRepresentingANodeImport()
        {
            const string testXml = @"<node><id>1</id><label>Node 1</label><adjacentNodes><id>2</id><id>3</id></adjacentNodes></node>";
                          

            var nodeDeserializer = new NodeXmlDeserializer();
            var node = nodeDeserializer.FromXml(testXml);

            Assert.AreEqual(1, node.Id);
            Assert.AreEqual("Node 1", node.Label);
            Assert.AreEqual(new List<int>{2,3}, node.AdjacentNodesIds);
        }

        //Todo further test for invalid or empty string
    }
}