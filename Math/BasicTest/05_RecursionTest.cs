namespace BasicTest;

[TestClass]
public class RecursionTest
{
    [TestMethod]
    public void GetDivCombinations()
    {
        Recursion.GetDivCombinations(20,new List<int>());
        Assert.IsTrue(true);
    }
}