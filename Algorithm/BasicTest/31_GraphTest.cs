namespace BasicTest;

[TestClass]
public class GraphTest
{
    Graph graph;

    [TestInitialize]
    public void InitGraph()
    {
        int[][] array = new int[][]{
            new int[] {1,3}, //0
            new int[] {0,2,4}, //1
            new int[] {1,5}, //2
            new int[] {0,4}, //3
            new int[] {1,3,5,6}, //4
            new int[] {2,4,7},//5
            new int[] {4,7}, //6
            new int[] {5,6}};//7

        var v = array.Length;
        graph = new Graph(v);
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                graph.AddEdge(i, array[i][j]);
            }
        }
    }

    [TestMethod]
    [DataRow(0, 5)]
    [DataRow(0, 2)]
    public void TestBFS(int s, int t)
    {
        graph.BFS(s, t);
        Assert.IsTrue(true);
    }

    [TestMethod]
    [DataRow(0, 6)]
    [DataRow(1, 7)]
    public void TestDFS(int s, int t)
    {
        graph.DFS(s, t);
        Assert.IsTrue(true);
    }
}