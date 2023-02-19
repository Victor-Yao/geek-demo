namespace AdvancedTest;

[TestClass]
public class TopSortGraphTest
{
    DirectedGraph graph;

    [TestInitialize]
    public void InitDirectedGraph()
    {
        int[][] array = new int[][]{
            new int[] {1,3}, //0
            new int[] {2,4}, //1
            new int[] {}, //2
            new int[] {1,4}, //3
            new int[] {2}};//4

        var v = array.Length;
        graph = new DirectedGraph(v);
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                graph.AddEdge(i, array[i][j]);
            }
        }
    }

    [TestMethod]
    public void TestTopSortbyKahn()
    {
        graph.TopSortbyKahn();
        Assert.IsTrue(true);
    }

    [TestMethod]
    public void TestTopSortbyDFS()
    {
        graph.TopSortbyDFS();
        Assert.IsTrue(true);
    }
}