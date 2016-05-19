using System.Collections.Generic;
using System.Linq;
using GraphPathFinder.Interfaces;
using GraphPathFinder.Models;

namespace GraphPathFinder.Repositories
{
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