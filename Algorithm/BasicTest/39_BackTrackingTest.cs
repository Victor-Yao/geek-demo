namespace BasicTest;

[TestClass]
public class BackTrackingTest
{
    [TestMethod]
    [DataRow(8)]
    public void TestNQueensProblem(int number)
    {
        var eightQueens = new NQueensProblem(number);
        var queue = new NQueensProblem(number);
        eightQueens.CalNQueens(0);

        Assert.IsTrue(eightQueens.Total > 0);
        Console.WriteLine($"total:{eightQueens.Total}");
    }

    [TestMethod]
    [DataRow(5, 9, new int[] { 2, 2, 4, 6, 3 })]
    public void TestKnapsackProblem(int num, int weight, int[] items)
    {
        var kp = new KnapsackProblem();
        kp.f(0, 0, items, num, weight);

        Assert.AreEqual(9, kp.MaxW);
        // Console.WriteLine($"Count:{kp.Count}");
    }

    [TestMethod]
    [DataRow("test", "test")]
    [DataRow("test", "t?st")]
    [DataRow("tst", "t?st")]
    [DataRow("test", "t*t")]
    [DataRow("test", "*t")]
    [DataRow("test", "te*")]
    [DataRow("test1", "t?st")]
    public void TestPattern(string text, string pattern)
    {
        var p = new Pattern(pattern);
        var ret = p.Match(text);

        Assert.IsTrue(ret);
    }
}