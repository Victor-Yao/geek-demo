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

    [TestMethod]
    [DataRow("abcde", "ab")] // begin
    [DataRow("abcde", "bc")] // middle
    [DataRow("abcde", "de")] // end
    [DataRow("abcde", "er")] // mismatch
    [DataRow("abcde", "c")] // single
    [DataRow("abcde", "abcde")] // full
    public void TestBF(string a, string b)
    {
        var index = StringMatch.Bf(a, b);

        switch (b)
        {
            case "ab": 
                Assert.AreEqual(0, index);
                break;
            case "bc": 
                Assert.AreEqual(1, index);
                break;
            case "de": 
                Assert.AreEqual(3, index);
                break;
            case "er": 
                Assert.AreEqual(-1, index);
                break;
            case "c": 
                Assert.AreEqual(2, index);
                break;
            case "abcde": 
                Assert.AreEqual(0, index);
                break;
        }
    }
}