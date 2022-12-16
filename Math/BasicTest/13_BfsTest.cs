using System.IO;

namespace BasicTest;

[TestClass]
public class BreadthFirstSearchTest
{

    [TestMethod]
    public void TestDemoMethod()
    {
        string path = @"C:\Users\weyao\CodeRepos\GeekTime\Math\Basic";
        var attr = File.GetAttributes(path);
        // Assert.AreEqual(FileAttributes.Directory, attr);
        Assert.IsTrue(attr.HasFlag(FileAttributes.Directory));
    }

    [TestMethod]
    [DataRow(@"C:\Users\weyao\CodeRepos\GeekTime\Math")]
    public void TestBfsDirectoryByQueue(string path)
    {
        BreadthFirstSearch.BfsDirectoryByQueue(path);
        Assert.IsTrue(BreadthFirstSearch.Results.Count != 0);
        foreach (var entry in BreadthFirstSearch.Results)
        {
            Console.WriteLine(entry);
        }
    }

    [TestMethod]
    [DataRow(@"C:\Users\weyao\CodeRepos\GeekTime\Math")]
    public void TestDfsDirectoryByStack(string path)
    {
        BreadthFirstSearch.DfsDirectoryByStack(path);
        Assert.IsTrue(BreadthFirstSearch.Results.Count != 0);
        foreach (var entry in BreadthFirstSearch.Results)
        {
            Console.WriteLine(entry);
        }
    }

    [TestMethod]
    [DataRow(@"C:\Users\weyao\CodeRepos\GeekTime\Math")]
    public void TestDfsDirectoryByRecursion(string path)
    {
        BreadthFirstSearch.DfsDirectoryByRecursion(path);
        Assert.IsTrue(BreadthFirstSearch.Results.Count != 0);
        foreach (var entry in BreadthFirstSearch.Results)
        {
            Console.WriteLine(entry);
        }
    }
}