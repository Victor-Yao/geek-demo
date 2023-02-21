namespace BasicTest;

[TestClass]
public class HeapTest
{
    int[] a;
    [TestInitialize]
    public void InitTest()
    {
        a = new int[] { 0, 7, 5, 19, 8, 4, 1, 20, 13, 16 };
    }

    [TestMethod]
    public void TestMaxHeap()
    {
        var heap = new MaxHeap(a.Length);
        for (int i = 1; i < a.Length; i++)
        {
            heap.Insert(a[i]);
        }

        CollectionAssert.AreEqual(
            new int[] { 0, 20, 16, 19, 13, 4, 1, 7, 5, 8 },
            heap._a);

        heap.Remove();
        int[] ret = new int[heap._a.Length - 2];
        Array.Copy(heap._a, 1, ret, 0, ret.Length);
        CollectionAssert.AreEqual(
            new int[] { 19, 16, 8, 13, 4, 1, 7, 5 },
            ret);
    }

    [TestMethod]
    public void TestMinHeap()
    {
        var heap = new MinHeap(a.Length);
        for (int i = 1; i < a.Length; i++)
        {
            heap.Insert(a[i]);
        }
        CollectionAssert.AreEqual(
            new int[] { 0, 1, 5, 4, 8, 7, 19, 20, 13, 16 },
            heap._a);

        heap.Remove();
        int[] ret = new int[heap._a.Length - 2];
        Array.Copy(heap._a, 1, ret, 0, ret.Length);
        CollectionAssert.AreEqual(
            new int[] { 4, 5, 16, 8, 7, 19, 20, 13 },
            ret);
    }
}

[TestClass]
public class HeapSortTest
{
    int[] a;
    [TestInitialize]
    public void InitTest()
    {
        a = new int[] { 0, 7, 5, 19, 8, 4, 1, 20, 13, 16 };
    }

    [TestMethod]
    public void TestBuildHeap2()
    {
        HeapSort.BuildHeap2(a);
        CollectionAssert.AreEqual(
            new int[] { 0, 20, 16, 19, 13, 4, 1, 7, 5, 8 },
            a);
    }

    [TestMethod]
    public void TestSort()
    {
        HeapSort.Sort(a);
        // for (int i = 1; i < a.Length; i++)
        // {
        //     Console.Write($"{a[i]}, ");
        // }
        // Assert.IsTrue(true);
        CollectionAssert.AreEqual(
            new int[] { 0, 1, 4, 5, 7, 8, 13, 16, 19, 20 },
            a);
    }

    [TestMethod]
    public void TestBuildHeap1()
    {
        HeapSort.BuildHeap1(a);
        CollectionAssert.AreEqual(
            new int[] { 0, 20, 16, 19, 13, 4, 1, 7, 5, 8 },
            a);
    }
}