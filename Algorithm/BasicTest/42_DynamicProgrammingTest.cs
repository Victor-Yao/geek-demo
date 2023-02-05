namespace BasicTest;

[TestClass]
public class DP2Test
{
    [TestMethod]
    [DataRow("mitcmu", "mtacnu")]
    public void TestLwstBT(string a, string b)
    {
        var dp = new Dp2(a, b);
        dp.LwstBT(0, 0, 0);
        Assert.AreEqual(3, dp.MinDist);
    }

    [TestMethod]
    [DataRow("mitcmu", "mtacnu")]
    public void TestLwstDP(string a, string b)
    {
        var ret = Dp2.LwstDP(a, b);
        Assert.AreEqual(3, ret);
    }

    [TestMethod]
    [DataRow("mitcmu", "mtacnu")]
    public void TestLcsDP(string a, string b)
    {
        var ret = Dp2.LcsDP(a, b);
        Assert.AreEqual(4, ret);
    }

    [TestMethod]
    [DataRow(new int[] { 2, 9, 3, 6, 5, 1, 7 })]
    public void TestGetLIS(int[] a)
    {
        var result = Dp2.LongestIS(a);

        Console.Write("Result: ");
        foreach (var item in result)
        {
            Console.Write(item + " ");
        }
        Assert.AreEqual(4, result.Count);
    }

    [TestMethod]
    [DataRow(new int[] { 2, 9, 3, 6, 5, 1, 7 })]
    public void TestGetLisBT(int[] a)
    {
        Dp2.LongestISBT(0, 0, a, "");

        if (!string.IsNullOrEmpty(Dp2.Result))
            Console.WriteLine($"Result: {Dp2.Result}");
        else
            Console.WriteLine("Result is invalid");

        Assert.AreEqual(4, Dp2.Result.Length);
    }

    [TestMethod]
    // [DataRow(new int[] { 2, 9, 3, 6, 5, 1, 7 })]
    [DataRow(new int[] { 3, 2, 1, 4 })]
    public void TestLongestISDP(int[] array)
    {
        int ret = Dp2.LongestISDP(array);
        Assert.AreEqual(2, ret);
    }
}