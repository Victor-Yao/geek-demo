namespace BasicTest;

[TestClass]
public class AcTest
{
    [TestMethod]
    [DataRow(new string[] { "abcd", "bcd", "bc", "c" })]
    public void TestBuildTireTree(string[] words)
    {
        var ac = new AC();
        ac.BuildTrieTree(words);

        Assert.AreEqual('h', ac.root.Children['h' - 'a'].Data);
    }

    [TestMethod]
    [DataRow(new string[] { "abcd", "bcd", "bc", "c" })]
    public void TestBuildFailurePointer(string[] words)
    {
        var ac = new AC();
        ac.BuildTrieTree(words);
        ac.BuildFailurePointer();

        Assert.AreSame(ac.root.Children[1].Children[2],
                        ac.root.Children[0].Children[1].Children[2].Fail);
    }

    [TestMethod]
    [DataRow(new string[] { "abcd", "bcd", "bc", "c" }, "bc")]
    public void TestMatch(string[] words, string text)
    {
        var ac = new AC();
        ac.BuildTrieTree(words);
        ac.BuildFailurePointer();
        ac.Match(text);

        Assert.IsTrue(true);
    }
}