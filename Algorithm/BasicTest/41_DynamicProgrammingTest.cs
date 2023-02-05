namespace BasicTest;

[TestClass]
public class DP1Test
{
    int[,] matrix = new int[,] { { 1, 3, 5, 9 }, { 2, 1, 3, 4 }, { 5, 2, 6, 7 }, { 6, 8, 4, 3 } };

    [TestMethod]
    public void TestMinDistBT()
    {
        var dp = new Dp1();
        dp.MinDistBT(0, 0, 0, matrix, 4);
        Assert.AreEqual(19, dp.MinDist);
    }

    [TestMethod]
    public void TestMinDistDP()
    {
        var dp = new Dp1();
        var ret = dp.MinDistDP(matrix, 4);
        Assert.AreEqual(19, ret);
    }

    [TestMethod]
    public void TestMinDistDP1()
    {
        var dp = new Dp1();
        // (n-1, n-1)
        var ret = dp.MinDistDP1(3, 3, matrix);
        Assert.AreEqual(19, ret);
    }


}