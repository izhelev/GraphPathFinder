using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using GraphPathFinder.Models;
using Moq;
using NUnit.Framework;

namespace GraphPathFinder.UnitTests
{
    [TestFixture]
    public class NodeImportServiceTestFixtures
    {

        private Mock<INodeRepository> _nodeRepository;

        [SetUp]
        public void Setup()
        {
             _nodeRepository = new Mock<INodeRepository>();
        }


        private NodeImportService GetNodeImporterInstance(INodeRepository nodeRepository)
        {
            return new NodeImportService(nodeRepository);
        }

        //If new node
        [TestCase]
        public void GivenANewNodeInsertItIntoTheDataStore()
        {
            var nodeImportService = GetNodeImporterInstance(_nodeRepository.Object);

            var newNode = new Node()
            {
                Id = 1,
                AdjacentNodesIds = new List<int>(){2, 3},
                Label = "New Node"
            };


            nodeImportService.Import(new List<Node>() { newNode });

            _nodeRepository.Verify(f => f.Insert(newNode), Times.Once);      
        }

        //If deleted remove
         [TestCase]
        public void IfANodeDoesntAppearInTheListDeleteFromDataStore()
        {

            var nodeToDelete = new Node() {Id = 1};
            var nodeImportService = GetNodeImporterInstance(_nodeRepository.Object);

            _nodeRepository.Setup(f => f.GetAll()).Returns(new List<Node>(){nodeToDelete});

            nodeImportService.Import(new List<Node>() {});

            _nodeRepository.Verify(f => f.Delete(nodeToDelete.Id), Times.Once);  
        }



        //If update update - not clear if this is a requirement so leaving this for now
         

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
        public void Import(List<Node> newNodes)
        {
            var existingNodes = _nodeRepository.GetAll().ToList();

            foreach (var node in newNodes)
            {
                //With a larger list will be slow. So replace this with a dictionary
                if (existingNodes.All(f => f.Id != node.Id))
                {
                    _nodeRepository.Insert(node);
                }
            }

            DeleteNodesThatDontExistAnymore(existingNodes, newNodes);
        }

        private void DeleteNodesThatDontExistAnymore(IEnumerable<Node> existingNodes, IEnumerable<Node> newNode)
        {
            foreach (var nodeToDelete in existingNodes.Where(en => newNode.All(nn => nn.Id != en.Id)))
            {
                _nodeRepository.Delete(nodeToDelete.Id);
            }
        }
    }
}
