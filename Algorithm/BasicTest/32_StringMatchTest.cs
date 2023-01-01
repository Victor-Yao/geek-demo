namespace BasicTest;

[TestClass]
public class StringMatchTest
{
    [TestMethod]
    [DataRow("cabcab","bc")]
    [DataRow("qwertasdfzxc","ert")]
    public void TestBM(string main, string pattern)
    {
        char[] a = main.ToCharArray();
        char[] b = pattern.ToCharArray();
        var n = a.Length;
        var m = b.Length;

        var ret = StringMatch.Bm(a, n, b, m);

        Assert.AreNotEqual(-1, ret);
        Console.WriteLine($"index: {ret}");
    }
}