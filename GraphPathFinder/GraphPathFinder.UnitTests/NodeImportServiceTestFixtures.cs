using System.Collections.Generic;
using GraphPathFinder.Interfaces;
using GraphPathFinder.Models;
using GraphPathFinder.Repositories;
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
}
