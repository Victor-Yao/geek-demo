namespace BasicTest;

[TestClass]
public class StringMatchTest
{
    [TestMethod]
    [DataRow("cabcab", "bc")]
    [DataRow("qwertasdfzxc", "ert")]
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

    [TestMethod]
    [DataRow("ababaeabac", "ababacd")]
    [DataRow("qwertasdfzxc", "ert")]
    public void TestKMP(string main, string pattern)
    {
        var index = StringMatch.Kmp(main, pattern);

        if(main == "ababaeabac")
            Assert.AreEqual(-1, index);
        else
            Assert.AreEqual(2, index);
    }
}