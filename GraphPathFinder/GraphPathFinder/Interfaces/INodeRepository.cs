using System.Collections.Generic;
using GraphPathFinder.Models;

namespace GraphPathFinder.Interfaces
{
    public interface INodeRepository
    {
        void Insert(Node node);
        void Update(Node node);
        void Delete(int id);
        IEnumerable<Node> GetAll();
    }
}