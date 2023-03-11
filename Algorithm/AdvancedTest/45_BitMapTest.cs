namespace AdvancedTest;

[TestClass]
public class BitMapTest
{

    [TestMethod]
    [DataRow(10,5)]
    [DataRow(100,58)]
    [DataRow(1000,999)]
    public void TestBitMap(int len, int k)
    {
        var bm = new BitMap(len);
        bm.Set(k);
        Assert.IsTrue(bm.Get(k));
        Assert.IsFalse(bm.Get(k-1));
    }

    [TestMethod]
    [DataRow(100,58)]
    [DataRow(1000,999)]
    public void TestBloomFilter(int len, int k)
    {
        var bf = new BloomFilter(len);
        bf.Set(k);
        Assert.IsTrue(bf.Get(k));
        Assert.IsFalse(bf.Get(k-1));
    }
}