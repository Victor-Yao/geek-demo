namespace BasicTest;

[TestClass]
public class MatrixDistanceTest
{
    int[,] matrix = new int[,] { { 1, 3, 5, 9 }, { 2, 1, 3, 4 }, { 5, 2, 6, 7 }, { 6, 8, 4, 3 } };

    [TestMethod]
    public void TestMinBT()
    {
        var md = new MatrixDistance();
        md.MinBT(0, 0, 0, matrix, 4);
        Assert.AreEqual(19, md.MinDist);
    }

    [TestMethod]
    public void TestMinDP()
    {
        var md = new MatrixDistance();
        var ret = md.MinDP(matrix, 4);
        Assert.AreEqual(19, ret);
    }

    [TestMethod]
    public void TestMinDP1()
    {
        var md = new MatrixDistance();
        // (n-1, n-1)
        var ret = md.MinDP1(3, 3, matrix);
        Assert.AreEqual(19, ret);
    }
}

[TestClass]
public class CoinNumberTest
{
    [TestMethod]
    [DataRow(new int[] { 1, 3, 5 }, 9)]
    public void TestBT(int[] coins, int w)
    {
        var coin = new CoinNumber();
        coin.BT(w, 0, coins);

        Console.WriteLine($"number: {coin.MinNum}");
        Assert.AreEqual(3, coin.MinNum);
    }

    [TestMethod]
    [DataRow(new int[] { 1, 3, 5 }, 9)]
    public void TestDP(int[] coins, int w)
    {
        var num = CoinNumber.DP(w);

        Console.WriteLine($"minimum number: {num}");
        Assert.AreEqual(3, num);
    }

    [TestMethod]
    [DataRow(new int[] { 1, 3, 5 }, 9)]
    public void TestDP1(int[] coins, int w)
    {
        var num = CoinNumber.DP1(w);

        Console.WriteLine($"minimum number: {num}");
        Assert.AreEqual(3, num);
    }
}