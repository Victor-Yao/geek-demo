namespace BasicTest;

[TestClass]
public class EditDistanceTest
{
    [TestMethod]
    [DataRow("mitcmu", "mtacnu")]
    public void TestLwstBT(string a, string b)
    {
        var dp = new EditDistance(a, b);
        dp.LwstBT(0, 0, 0);
        Assert.AreEqual(3, dp.MinDist);
    }

    [TestMethod]
    [DataRow("mitcmu", "mtacnu")]
    public void TestLwstDP(string a, string b)
    {
        var ret = EditDistance.LwstDP(a, b);
        Assert.AreEqual(3, ret);
    }

    [TestMethod]
    [DataRow("mitcmu", "mtacnu")]
    public void TestLcsDP(string a, string b)
    {
        var ret = EditDistance.LcsDP(a, b);
        Assert.AreEqual(4, ret);
    }
}

[TestClass]
public class LongestISTest
{
    
    [TestMethod]
    [DataRow(new int[] { 2, 9, 3, 6, 5, 1, 7 })]
    public void TestGetLIS(int[] a)
    {
        var list = LongestIS.GetLIS(a);

        Console.Write("Result: ");
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Assert.AreEqual(4, list.Count);
    }

    [TestMethod]
    [DataRow(new int[] { 2, 9, 3, 6, 5, 1, 7 })]
    public void TestLongestISBT(int[] a)
    {
        LongestIS.LongestISBT(0, 0, a, "");

        if (!string.IsNullOrEmpty(LongestIS.Result))
            Console.WriteLine($"Result: {LongestIS.Result}");
        else
            Console.WriteLine("Result is invalid");

        Assert.AreEqual(4, LongestIS.Result.Length);
    }

    [TestMethod]
    // [DataRow(new int[] { 2, 9, 3, 6, 5, 1, 7 })]
    [DataRow(new int[] { 3, 2, 1, 4 })]
    public void TestLongestISDP(int[] array)
    {
        int ret = LongestIS.LongestISDP(array);
        Assert.AreEqual(2, ret);
    }
}