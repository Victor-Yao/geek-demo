namespace BasicTest;

[TestClass]
public class EditDistanceTest
{
    [TestMethod]
    public void TestGetStrDistance()
    {
        var distance = EditDistance.GetStrDistance("aaa", "aab");
        Console.WriteLine($"distance: {distance}");
        Assert.AreEqual(1,distance);
    }
}