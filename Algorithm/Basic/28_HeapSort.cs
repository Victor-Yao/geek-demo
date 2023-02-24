namespace Basic;

/// <summary>
/// Max Heap
/// a[i] > a[2i] || a[2i + 1] 
/// </summary>
public class Heap
{
    public int[] _a; // index starts from 1
    internal int _n; // maxmium element number
    internal int _count; // element number stores in the heap

    public Heap(int capacity)
    {
        _a = new int[capacity];
        _n = capacity;
        _count = 0;
    }

    public virtual void Insert(int data) { }

    public virtual void Remove() { }
}

public class MaxHeap : Heap
{
    public MaxHeap(int capacity) : base(capacity)
    {
    }

    public override void Insert(int data)
    {
        if (_count >= _n)
            return; // heap is full

        ++_count;
        _a[_count] = data;
        int i = _count;
        while (i / 2 > 0 && _a[i] > _a[i / 2])// heapify from bottom to top
        {
            HeapSort.Swap(_a, i, i / 2);
            i = i / 2;
        }
    }

    public override void Remove()
    {
        if (_count == 0)
            return;

        _a[1] = _a[_count];
        --_count;
        HeapSort.Heapify(_a, _count, 1); // heapify max heap, top -> bottom
    }
}

public class MinHeap : Heap
{
    public MinHeap(int capacity) : base(capacity)
    {

    }

    public override void Insert(int data)
    {
        if (_count >= _n)
            return; // heap is full

        ++_count;
        _a[_count] = data;
        int i = _count;
        while (i / 2 > 0 && _a[i] < _a[i / 2])// Heapify Min Heap, bottom -> top 
        {
            HeapSort.Swap(_a, i, i / 2);
            i = i / 2;
        }
    }

    public override void Remove()
    {
        if (_count == 0)
            return;

        _a[1] = _a[_count];
        --_count;
        MinHeapify(_a, _count, 1); // heapify max heap, top -> bottom
    }

    /// <summary>
    /// Heapify Min Heap, top -> bottom 
    /// </summary>
    private void MinHeapify(int[] a, int n, int i)
    {
        int minPos = i;
        while (true)
        {
            if (i * 2 <= n && a[i] > a[i * 2])
                minPos = i * 2;
            if (i * 2 + 1 <= n && a[minPos] > a[i * 2 + 1])
                minPos = i * 2 + 1;
            if (minPos == i)
                break;

            HeapSort.Swap(a, i, minPos);
            i = minPos;
        }
    }
}

/// <summary>
/// Max Heap Sort algorithm. It contains Build Heap and Sort 
/// </summary>
public static class HeapSort
{

    /// <summary>
    /// handle array element from head to end
    /// </summary>
    /// Param:
    ///   a: build heap on a
    public static void BuildHeap1(int[] a)
    {
        for (int i = 1; i < a.Length; i++)
        {
            while (i / 2 > 0 && a[i] > a[i / 2])// heapify: bottom -> top
            {
                Swap(a, i, i / 2);
                i = i / 2;
            }
        }
    }

    /// <summary>
    /// handle array element from end to head
    /// </summary>
    /// Param:
    ///   a: build heap on a
    public static void BuildHeap2(int[] a)
    {
        var n = a.Length - 1;
        for (int i = n / 2; i >= 1; i--)
        {
            Heapify(a, n, i);
        }
    }

    /// <summary>
    /// Heap Sort
    /// </summary>
    /// Param:
    ///   a: array
    public static void Sort(int[] a)
    {
        // BuildHeap1(a);
        BuildHeap2(a);
        int k = a.Length - 1;
        while (k > 1)
        {
            Swap(a, 1, k);
            --k;
            Heapify(a, k, 1);
        }
    }

    /// <summary>
    /// heapify: top -> bottom
    /// </summary>
    /// Param:
    ///   a: array
    ///   n: element number, a.length-1
    ///   i: current element index
    public static void Heapify(int[] a, int n, int i)
    {
        int maxPos = i;
        while (true)
        {
            if (i * 2 <= n && a[i] < a[i * 2])
                maxPos = i * 2;
            if (i * 2 + 1 <= n && a[maxPos] < a[i * 2 + 1])
                maxPos = i * 2 + 1;
            if (maxPos == i)
                break;

            Swap(a, i, maxPos);
            i = maxPos;
        }
    }

    public static void Swap(int[] a, int i, int j)
    {
        int temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }
}