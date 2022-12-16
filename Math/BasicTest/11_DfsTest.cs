namespace BasicTest;

[TestClass]
public class DepthFirstSearchTest
{
    TreeNode root = new TreeNode('\0', string.Empty, string.Empty);
    

    [TestMethod]
    [DataRow("geek")]
    [DataRow("geometry")]
    public void TestBuildDictionaryTree(string word)
    {
        var ret = DepthFirstSearch.BuildDictionaryTree(word.ToCharArray(), root);
        Assert.IsTrue(ret, $"Expected for true; Actual is {ret}");
    }

    [TestMethod]
    [DataRow("geek")]
    [DataRow("geometry")]
    public void TestDfsByRecursion(string word)
    {
        var ret = DepthFirstSearch.BuildDictionaryTree(word.ToCharArray(), root);
        Assert.IsTrue(ret, $"Expected for true; Actual is {ret}");

        var exp = DepthFirstSearch.DfsByRecursion(word.ToCharArray(), root);
        Assert.AreEqual("has explaination", exp);
    }

    [TestMethod]
    public void TestDfsByStack()
    {
        string[] words = { "geek", "geometry" };
        foreach (var word in words)
        {
            DepthFirstSearch.BuildDictionaryTree(word.ToCharArray(), root);
        }

        DepthFirstSearch.DfsByStack(root);
    }
}