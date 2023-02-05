namespace BasicTest;

[TestClass]
public class DPTest
{
    [TestMethod]
    [DataRow(new int[] { 2, 2, 4, 6, 3 }, 5, 9)]
    public void TestKnapsack(int[] weight, int n, int w)
    {
        var dp = new Dp();
        var ret = dp.Knapsack(weight, n, w);
        Assert.AreEqual(9, ret);
    }

    [TestMethod]
    [DataRow(new int[] { 2, 2, 4, 6, 3 }, 5, 9)]
    public void TestKnapsack2(int[] weight, int n, int w)
    {
        var dp = new Dp();
        var ret = dp.Knapsack2(weight, n, w);
        Assert.AreEqual(9, ret);
    }


    [TestMethod]
    [DataRow(new int[] { 2, 2, 4, 6, 3 }, new int[] { 3, 4, 8, 9, 6 }, 9)]
    public void TestBTKnapsack(int[] weights, int[] values, int wLimit)
    {
        var bt = new BT(weights, values, wLimit);
        bt.f(0, 0, 0);
        Assert.AreEqual(18, bt.MaxV);
    }

    [TestMethod]
    [DataRow(new int[] { 2, 2, 4, 6, 3 }, 9)]
    public void TestBTKnapsack1(int[] weights, int wLimit)
    {
        var bt = new BT(weights, null, wLimit);
        bt.f1(0, 0);
        Assert.AreEqual(18, bt.MaxV);
    }

    [TestMethod]
    [DataRow(new int[] { 2, 2, 4, 6, 3 }, new int[] { 3, 4, 8, 9, 6 }, 9)]
    public void TestKnapsack3(int[] weights, int[] values, int w)
    {
        var dp = new Dp();
        var maxV = dp.Knapsack3(weights, values, weights.Length, w);
        Assert.AreEqual(18, maxV);
    }
}