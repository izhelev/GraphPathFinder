namespace GraphPathFinder.UnitTests
{
    public interface IShortPathCalculator
    {
        int[] GetPath(int startNodeId, int endNodeId);
    }
}