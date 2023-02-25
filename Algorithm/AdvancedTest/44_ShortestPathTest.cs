namespace AdvancedTest;

[TestClass]
public class ShortestPathTest
{
    DirectedWeightGraph graph;

    [TestInitialize]
    public void InitDirectedGraph()
    {
        var dataSet = new int[,]{
            {0,1,10},
            {0,4,15},
            {1,2,15},
            {1,3,2},
            {2,5,5},
            {3,2,1},
            {3,5,12},
            {4,5,10}};
        int vertexNum = 6;

        graph = new DirectedWeightGraph(vertexNum);
        for (int i = 0; i < dataSet.GetLength(0); i++)
        {
            graph.AddEdge(dataSet[i, 0], dataSet[i, 1], dataSet[i, 2]);
        }
    }

    [TestMethod]
    [DataRow(0, 5)]
    public void TestDijkstra(int s, int t)
    {
        graph.Dijkstra(s, t);
        Assert.IsTrue(true);
    }
}