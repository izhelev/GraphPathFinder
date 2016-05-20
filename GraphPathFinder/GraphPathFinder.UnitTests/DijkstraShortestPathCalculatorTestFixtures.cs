using System.Collections.Generic;
using GraphPathFinder.Models;
using GraphPathFinder.ShortestPathAlgorithsm;
using NUnit.Framework;

namespace GraphPathFinder.UnitTests
{
  
    [TestFixture]
    public class DijkstraShortestPathCalculatorTestFixtures
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

        private List<Node> SimpleGraphWithNodeTwoUnconected() 
        {
             return new List<Node>()
            {
                new Node(){Id = 1, AdjacentNodesIds = new List<int>(){1,3}},
                new Node(){Id = 2, AdjacentNodesIds = new List<int>()},
                new Node(){Id = 3, AdjacentNodesIds = new List<int>(){3}}
            };
        }


        [TestCase(1, 3, new int[] { 1,2,3})]
        [TestCase(2, 3, new int[] { 2, 3 })]
        [TestCase(1, 2, new int[] { 1, 2 })]
        public void GivenASimpleGraphCalculateShortestPath(int startingNode, int endingNode, int[] expectedPathIds)
        {
            var shortestPathCalculator = new DijkstrasShortestPathCalculator(GetSimpleGraphNodes());
            var result = shortestPathCalculator.GetPath(startingNode, endingNode);

            Assert.AreEqual(expectedPathIds, result);
        }

        [TestCase(1, 2, new int[] { })]
        public void IfStartAndEndNodesAreUnconnectedReturnAnEmptyArray(int startingNode, int endingNode, int[] expectedPathIds)
        {
            
            var shortestPathCalculator = new DijkstrasShortestPathCalculator(SimpleGraphWithNodeTwoUnconected());
            var result = shortestPathCalculator.GetPath(startingNode, endingNode);

            Assert.AreEqual(expectedPathIds, result);
        } 
    }
}
