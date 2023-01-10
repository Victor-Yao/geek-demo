namespace BasicTest;

[TestClass]
public class TrieTest
{
    [TestMethod]
    [DataRow(new string[] { "hello", "her", "hi", "see", "so" }, "her")]
    public void TestFind(string[] texts, string pattern)
    {
        var trie = new Trie();

        foreach (var text in texts)
        {
            trie.Insert(text);
        }

        var ret = trie.Find(pattern);
        Assert.IsTrue(ret);
    }
}