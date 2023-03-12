namespace AdvancedTest;

[TestClass]
public class _AStarSearchTest
{

    GameMap map;

    [TestMethod]
    [DataRow(0, 10)]
    public void TestAStarSearch(int s, int t)
    {
        map.AStar(s, t);
        Assert.IsTrue(true);
    }

    [TestInitialize]
    public void InitGameMap()
    {
        // {id, x, y}
        var vertexes = new int[,]{
            {0,320,630},
            {1,300,630},
            {2,280,625},
            {3,270,630},
            {4,320,700},
            {5,360,620},
            {6,320,590},
            {7,370,580},
            {8,350,730},
            {9,390,690},
            {10,400,620},
            {11,400,560},
            {12,270,670},
            {13,270,600}};

        // undirected weight edges {s,t,w}
        var edges = new int[,]{
            {0,1,20},
            {0,4,60},
            {0,5,60},
            {0,6,60},
            {1,0,20},
            {1,2,20},
            {2,1,20},
            {2,3,10},
            {3,2,10},
            {3,12,40},
            {3,13,30},
            {4,0,60},
            {4,8,50},
            {4,12,40},
            {5,0,60},
            {5,8,70},
            {5,9,80},
            {5,10,50},
            {6,0,60},
            {6,7,70},
            {6,13,50},
            {7,6,70},
            {7,11,50},
            {8,4,50},
            {8,5,70},
            {8,9,500},
            {9,5,80},
            {9,8,50},
            {9,10,60},
            {10,5,50},
            {10,9,60},
            {10,11,60},
            {11,7,50},
            {11,10,60},
            {12,3,40},
            {12,4,40},
            {13,3,30},
            {13,6,50}};

        int v = vertexes.GetLength(0);
        map = new GameMap(v);
        for (int i = 0; i < v; i++)
        {
            map.AddVertex(vertexes[i, 0], vertexes[i, 1], vertexes[i, 2]);
        }

        for (int i = 0; i < edges.GetLength(0); i++)
        {
            map.AddEdge(edges[i, 0], edges[i, 1], edges[i, 2]);
        }
    }
}