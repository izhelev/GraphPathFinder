using System.Collections.Generic;
using System.Linq;
using GraphPathFinder.Models;
using NUnit.Framework;

namespace GraphPathFinder.UnitTests
{
  
    [TestFixture]
    public class ShortestPathCalculatorTestFixtures
    {
        private List<Node> GetSimpleGraphNodes()
        {
            return new List<Node>()
            {
                new Node(){Id = 1, AdjacentNodesIds = new List<int>(){1, 2}},
                new Node(){Id = 2, AdjacentNodesIds = new List<int>(){3}},
                new Node(){Id = 3, AdjacentNodesIds = new List<int>(){2}}
            };
        }


        [TestCase(1, 3, new int[] { 1,2,3})]
        [TestCase(2, 3, new int[] { 2, 3 })]
        [TestCase(1, 2, new int[] { 1, 2 })]
        public void GivenASimpleGraphCalculateShortestPath(int startingNode, int endingNode, int[] expectedPathIds)
        {
            var shortestPathCalculator = new DijkstraShortestPathCalculator(GetSimpleGraphNodes());
            var result = shortestPathCalculator.GetPath(startingNode, endingNode);

            Assert.AreEqual(expectedPathIds, result);
        } 
    }


    public class DijkstraShortestPathCalculator : IShortPathCalculator
    {
        private readonly List<Node> _nodesInMap;

        private readonly Dictionary<int, Node> _indexedNodes;

        public DijkstraShortestPathCalculator(List<Node> nodesInMap)
        {
            _nodesInMap = nodesInMap;            ;
            _indexedNodes = InitGraph(nodesInMap);
        }

        private Dictionary<int, Node> InitGraph(IEnumerable<Node> nodesInMap)
        {
            var indexedNodes = new Dictionary<int, Node>();
            foreach (var node in nodesInMap)
            {
                indexedNodes[node.Id] = node;
            }
            return indexedNodes;
        }

        public int[] GetPath(int startNodeId, int endNodeId)
        {
            var path = new List<int>();
         
            var nodesToVisit = _nodesInMap;
            var distances = InitDistances(_nodesInMap, startNodeId);

            while (nodesToVisit.Count() != 0)
            {
                int currentNodeId = distances.Where(v => nodesToVisit.Any(n => n.Id == v.Key))
                    .OrderBy(v => v.Value)
                    .Select(k => k.Key)
                    .First();

                var currentNode = _indexedNodes[currentNodeId];

                foreach (var adjacentNode in currentNode.AdjacentNodesIds)
                {
                    var newDistance = distances[currentNode.Id] + 1;
                    if (newDistance < distances[adjacentNode])
                    {
                        distances[adjacentNode] = newDistance;
                    }
                    
                }

                nodesToVisit.Remove(currentNode);
                              
                path.Add(currentNodeId);

                if (currentNodeId == endNodeId)
                {
                    break;
                }
                            
            }

            return path.ToArray();
        }

        private Dictionary<int, int> InitDistances(IEnumerable<Node> nodes, int startNodeId)
        {
            var distances = new Dictionary<int, int>();

            foreach (var node in nodes)
            {
                if (node.Id == startNodeId)
                {
                    distances[node.Id] = 0;
                }
                else
                {
                    distances[node.Id] = int.MaxValue;
                }
            }

            return distances;
        }
       

    }

    public interface IShortPathCalculator
    {
        int[] GetPath(int startNodeId, int endNodeId);
    }
}
