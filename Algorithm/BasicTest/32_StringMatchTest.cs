namespace BasicTest;

[TestClass]
public class StringMatchTest
{
    [TestMethod]
    [DataRow("cabacab", "bc")]
    [DataRow("qwertasdfzxc", "ert")]
    public void TestBM(string a, string b)
    {
        var index = StringMatch.Bm(a, b);

        if(a=="cabacab")
            Assert.AreEqual(-1, index);
        else
            Assert.AreEqual(2, index);
    }

    [TestMethod]
    [DataRow("ababaeabac", "ababacd")]
    [DataRow("qwertasdfzxc", "ert")]
    public void TestKMP(string main, string pattern)
    {
        var index = StringMatch.Kmp(main, pattern);

        if (main == "ababaeabac")
            Assert.AreEqual(-1, index);
        else
            Assert.AreEqual(2, index);
    }
}