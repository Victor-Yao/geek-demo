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
    [DataRow(3, 50)]
    public void TestKnapsackProblem(int num, int weight)
    {
        var kp = new KnapsackProblem();
        var items = new int[num];
        var rand = new Random();
        for (int i = 0; i < num; i++)
        {
            items[i] = rand.Next(weight / 2);
        }

        kp.f(0, 0, items, num, weight);

        Assert.AreNotEqual(int.MinValue, kp.MaxW);
        Console.WriteLine($"max weight: {kp.MaxW}");
    }

    [TestMethod]
    // [DataRow("test", "test")]
    // [DataRow("test", "t?st")]
    // [DataRow("tst", "t?st")]
    // [DataRow("test", "t*t")]
    // [DataRow("test", "*t")]
    [DataRow("test", "te*")]
    // [DataRow("test1", "t?st")]
    public void TestPattern(string text, string pattern)
    {
        var p = new Pattern(pattern);
        var ret = p.Match(text);

        Assert.IsTrue(ret);
    }
}