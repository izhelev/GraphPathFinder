namespace GraphPathFinder.Interfaces
{
    public interface IShortPathCalculator
    {
        int[] GetPath(int startNodeId, int endNodeId);
    }
}