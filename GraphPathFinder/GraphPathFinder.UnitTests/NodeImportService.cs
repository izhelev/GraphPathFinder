using System.Collections.Generic;
using System.Linq;
using GraphPathFinder.Models;
using Moq;
using NUnit.Framework;

namespace GraphPathFinder.UnitTests
{
    [TestFixture]
    public class NodeImportServiceTestFixtures
    {
      
        [TestCase]
        public void GivenANewNodeInsertItIntoTheDataStore()
        {
            var nodeRepository = new Mock<INodeRepository>();

            var newNode = new Node()
            {
                Id = 1,
                AdjacentNodesIds = new List<int>(){2, 3},
                Label = "New Node"
            };

            var nodeImportService = new NodeImportService(nodeRepository.Object);
            nodeImportService.Import(new List<Node>(){newNode});

            nodeRepository.Verify(f=> f.Insert(newNode), Times.Once);      
        }

        //If deleted remove
        public void IfANodeDoesntAppearInTheListDeleteFromDataStore()
        {
            
        }



        //If update update
    }

    public interface INodeRepository
    {
        void Insert(Node node);
        void Update(Node node);
        void Delete(int id);
        IEnumerable<Node> GetAll();
    }

    public class NodeImportService
    {
        private readonly INodeRepository _nodeRepository;

        public NodeImportService(INodeRepository nodeRepository)
        {
            _nodeRepository = nodeRepository;
        }
        public void Import(List<Node> nodes)
        {
            var existingNodes = _nodeRepository.GetAll().ToList();

            foreach (var node in nodes)
            {
                //With a larger list will be slow. So replace this with a dictionary
                if (existingNodes.All(f => f.Id != node.Id))
                {
                    _nodeRepository.Insert(node);
                }
            }
        }
    }
}
