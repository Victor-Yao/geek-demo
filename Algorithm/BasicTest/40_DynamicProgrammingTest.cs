namespace BasicTest;

[TestClass]
public class KnapsackTest
{
    [TestMethod]
    [DataRow(new int[] { 2, 2, 4, 6, 3 }, 9)]
    public void TestBT(int[] weights, int wLimit)
    {
        var ks = new Knapsack(weights, wLimit);
        ks.BT(0, 0);
        Assert.AreEqual(9, ks.MaxW);
    }

    [TestMethod]
    [DataRow(new int[] { 2, 2, 4, 6, 3 }, 5, 9)]
    public void TestDP2D(int[] weight, int n, int w)
    {
        var ret = Knapsack.DP2D(weight, n, w);
        Assert.AreEqual(9, ret);
    }

    [TestMethod]
    [DataRow(new int[] { 2, 2, 4, 6, 3 }, 5, 9)]
    public void TestDP1D(int[] weight, int n, int w)
    {
        var ret = Knapsack.DP1D(weight, n, w);
        Assert.AreEqual(9, ret);
    }
}

[TestClass]
public class AdvancedKnapsackTest
{
    [TestMethod]
    [DataRow(new int[] { 2, 2, 4, 6, 3 }, new int[] { 3, 4, 8, 9, 6 }, 9)]
    public void TestBT(int[] weights, int[] values, int wLimit)
    {
        var ak = new AdvancedKnapsack(weights, values, wLimit);
        ak.BT(0, 0, 0);
        Assert.AreEqual(18, ak.MaxV);
    }

    [TestMethod]
    [DataRow(new int[] { 2, 2, 4, 6, 3 }, new int[] { 3, 4, 8, 9, 6 }, 9)]
    public void TestDP(int[] weights, int[] values, int w)
    {
        var maxV = AdvancedKnapsack.DP(weights, values, weights.Length, w);
        Assert.AreEqual(18, maxV);
    }
}

[TestClass]
public class YanghuiTriangeTest
{
    

    int[][] triangle = new int[5][]{
        new int[] {5},
        new int[] {7, 8},
        new int[] {2, 3, 4},
        new int[] {4, 9, 6, 1},
        new int[] {2, 7, 9, 4, 5}};

    [TestMethod]
    public void TestTriangleGreedy()
    {
        var ret = YanghuiTriangle.Greedy(triangle);
        Assert.AreEqual(20, ret);
    }

    [TestMethod]
    public void TestTriangleBT()
    {
        var tri = new YanghuiTriangle();
        tri.BT(0, 0, 0, triangle);
        Assert.AreEqual(20, tri.MinLen);
    }

    [TestMethod]
    public void TestTriangleDP1()
    {
        var ret = YanghuiTriangle.DP1(triangle);
        Assert.AreEqual(20, ret);
    }

    [TestMethod]
    public void TestTriangleDP2()
    {
        var ret = YanghuiTriangle.DP2(triangle);
        Assert.AreEqual(20, ret);
    }

    int[,] matrix = new int[,] { { 5, 0, 0, 0, 0 }, { 7, 8, 0, 0, 0 }, { 2, 3, 4, 0, 0 }, { 4, 9, 6, 1, 0 }, { 2, 7, 9, 4, 5 } };

    [TestMethod]
    public void TestMatrixDP1()
    {
        var ret = YanghuiTriangle.DP1(matrix);
        Assert.AreEqual(20, ret);
    }

    [TestMethod]
    public void TestMatrixDP2()
    {
        var ret = YanghuiTriangle.DP2(matrix);
        Assert.AreEqual(20, ret);
    }
}
