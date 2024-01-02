namespace Basic;

/// <summary>
/// Basic 0-1 knapsack problem 
/// </summary>
public class Knapsack
{
    private int[] _weights;
    private int _n; // total number of items
    private int _w; // weight limit

    public Knapsack(int[] weights, int wLimit)
    {
        _weights = weights;
        _n = weights.Length;
        _w = wLimit;
        _mem = new bool[_n, _w + 1];
    }

    private bool[,] _mem;
    public int MaxW { set; get; } = int.MinValue;
    /// <summary>
    /// Back Tracking with Mem
    /// </summary>
    /// Param:
    ///   i: the index of item need to be checked weither load or not.
    ///   cw: current weight
    ///   cv: current value
    /// Return:
    ///     nod return
    /// call example:
    ///     f(0,0,0)
    public void BT(int i, int cw)
    {
        if (cw == _w || i == _n)
        {
            if (cw > MaxW)
                MaxW = cw;
            return;
        }

        if (_mem[i, cw]) // 重复状态
            return;
        _mem[i, cw] = true;

        BT(i + 1, cw); // don't add i
        if (cw + _weights[i] <= _w)
            BT(i + 1, cw + _weights[i]); // add i
    }

    /// <summary>
    /// 2D array solution of Dynamic Programming
    /// </summary>
    /// Param:
    ///   weight: items weight array
    ///   n: number of items
    ///   w: weight limit
    /// Return:
    ///     maximum weight sum
    public static int DP2D(int[] weight, int n, int w)
    {
        bool[,] states = new bool[n, w + 1];
        states[0, 0] = true; // 第一行数据需要特殊处理，可以利用哨兵
        if (weight[0] < w)
        {
            states[0, weight[0]] = true;
        }

        for (int i = 1; i < n; i++) // 动态规划转移
        {
            for (int j = 0; j <= w; j++) // 不把第i个物品放入背包
            {
                if (states[i - 1, j] == true)
                    states[i, j] = states[i - 1, j];
            }

            for (int j = 0; j <= w - weight[i]; j++)// 把第i个物品放入背包
            {
                if (states[i - 1, j] == true)
                    states[i, j + weight[i]] = true;
            }
        }

        for (int i = w; i >= 0; i--) // output result
        {
            if (states[n - 1, i] == true)
                return i;
        }

        return 0;
    }

    /// <summary>
    /// 1D array solution of Dynamic Programming
    /// </summary>
    /// Param:
    ///   weight: items weight array
    ///   n: number of items
    ///   w: weight limit
    /// Return:
    ///     maximum weight sum
    public static int DP1D(int[] weight, int n, int w)
    {
        var states = new bool[w + 1];
        states[0] = true;
        if (weight[0] <= w)
        {
            states[weight[0]] = true;
        }

        for (int i = 1; i < n; i++) // 动态规划转移
        {
            for (int j = w - weight[i]; j >= 0; --j) //把第i个物品放入背包
            {
                if (states[j] == true)
                    states[j + weight[i]] = true;
            }
        }

        for (int i = w; i >= 0; --i)
        {
            if (states[i] == true)
                return i;
        }

        return 0;
    }
}

/// <summary>
/// advanced 0-1 knapsack problem
/// </summary>
public class AdvancedKnapsack
{

    public int MaxV { get; set; } = int.MinValue;
    private int[] _weights;
    private int[] _values;
    private int _n; // total number of items
    private int _w; // weight limit

    public AdvancedKnapsack(int[] weights, int[] values, int wLimit)
    {
        _weights = weights;
        _values = values;
        _n = weights.Length;
        _w = wLimit;
    }

    /// <summary>
    /// advanced 0-1 knapsack problem. Back Tracking solution.
    /// BT(0,0,0)
    /// </summary>
    /// Param:
    ///   i: the index of item need to be checked weither load or not.
    ///   cw: current weight
    ///   cv: current value
    public void BT(int i, int cw, int cv)
    {
        if (cw == _w || i == _n)
        {
            if (cv > MaxV)
                MaxV = cv;

            return;
        }

        BT(i + 1, cw, cv); // don't add i 

        if (cw + _weights[i] <= _w)
        {
            BT(i + 1, cw + _weights[i], cv + _values[i]); // add i
        }
    }

    /// <summary>
    /// Dynamic Programming solution
    /// </summary>
    /// Param:
    ///   weight: items weight array
    ///   values: items weight array
    ///   n: number of items
    ///   w: weight limit
    /// Return:
    ///     maximum weight
    public static int DP(int[] weights, int[] values, int n, int w)
    {
        var states = new int[n, w + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < w + 1; j++)
            {
                states[i, j] = -1;
            }
        }

        states[0, 0] = 0;
        if (weights[0] < w)
            states[0, weights[0]] = values[0];

        for (int i = 1; i < n; i++) // 动态规划, 状态转移
        {
            for (int j = 0; j <= w; j++) // don't add i
            {
                if (states[i - 1, j] >= 0)
                    states[i, j] = states[i - 1, j];
            }

            for (int j = 0; j <= w - weights[i]; j++) // add i
            {
                if (states[i - 1, j] >= 0)
                {
                    int v = states[i - 1, j] + values[i];
                    if (v > states[i, j + weights[i]])
                        states[i, j + weights[i]] = v;
                }
            }
        }

        //找出最大值
        int maxValue = -1;
        for (int j = 0; j <= w; j++)
        {
            if (states[n - 1, j] > maxValue)
                maxValue = states[n - 1, j];
        }

        return maxValue;
    }
}

/// <summary>
/// 
/// </summary>
public class ShoppingCart
{
    /// <summary>
    /// shopping cart discount problem (Double 11)- select 'n' goods from cart make the cost sum minimum and meet discount condition, ie. over 200 get 50 free
    /// </summary>
    /// Param:
    ///   prices: goods price array
    ///   lowerLimit: discount condition, over 200 get 50 free
    ///   w: discount condition
    public static void DP2D(int[] prices, int lowerLimit)
    {
        var limit = lowerLimit * 3;
        var states = new bool[prices.Length, limit + 1];// row: item number; col: price
        for (int i = 0; i < prices.Length; i++)
        {
            for (int j = 0; j < limit + 1; j++)
                states[i, j] = false;
        }

        states[0, 0] = true;
        if (prices[0] <= limit)
            states[0, prices[0]] = true;

        // status transfer
        for (int i = 1; i < prices.Length; i++)
        {
            for (int j = 0; j < limit + 1; j++)
            {
                if (states[i - 1, j] == true)
                    states[i, j] = states[i - 1, j];
            }

            for (int j = 0; j < limit - prices[i]; j++)
            {
                if (states[i - 1, j] == true)
                    states[i, j + prices[i]] = true;
            }
        }

        // output selected items
        int k = lowerLimit;
        for (; k < limit + 1; k++)
        {
            if (states[prices.Length - 1, k] == true)
                break;
        }

        if (k == limit + 1)
            Console.WriteLine("No result");
        else
            Console.WriteLine($"minimum total prices is {k}");

        for (int i = prices.Length - 1; i >= 1; i--)
        {
            if (k - prices[i] >= 0 && states[prices.Length - 1, k - prices[i]] == true)
            {
                Console.WriteLine($"Selected item id: {i}, price: {prices[i]}");
                k = k - prices[i];
            }
        }

        if (k != 0)
            Console.WriteLine($"Selected item id: {0}, price: {prices[0]}");
    }
}

/// <summary>
/// Exercise 40: caculate minimum path length of yanghui triangle.
/// </summary>
public class YanghuiTriangle
{
    /// <summary>
    /// Greedy solution
    /// minLen[i] = minLen[i-1] + min(matrix[i][j], matrix[i][j+1])
    /// </summary>
    /// Param:
    ///   matrix: yanghui triangle.
    /// Return:
    ///     int: minimum path length
    public static int Greedy(int[][] jagArray)
    {
        int[] minLen = new int[jagArray.Length];
        minLen[0] = jagArray[0][0];
        int j = 0;
        for (int i = 1; i < jagArray.Length; i++)
        {
            if (jagArray[i][j] > jagArray[i][j + 1])
            {
                j++;
                minLen[i] = minLen[i - 1] + jagArray[i][j + 1];
            }
            else
                minLen[i] = minLen[i - 1] + jagArray[i][j];//j doesn't change
        }

        return minLen[jagArray.Length - 1];
    }

    public int MinLen { get; set; } = int.MaxValue;
    /// <summary>
    /// back tracking 
    /// </summary>
    /// Param:
    ///   matrix: yanghui triangle.
    /// Return:
    ///     int: minimum path length
    public void BT(int i, int j, int temp, int[][] jagArray)
    {
        if (j > i)
            return;

        if (i == jagArray.GetLength(0))
        {
            if (temp < MinLen)
                MinLen = temp;
            return;
        }

        BT(i + 1, j, temp + jagArray[i][j], jagArray);

        BT(i + 1, j + 1, temp + jagArray[i][j], jagArray);
    }

    /// <summary>
    /// dynamic programming solution 1 
    /// S[i][j] = min(S[i-1][j],S[i-1][j-1]) + a[i][j]
    /// </summary>
    /// Param:
    ///   matrix: yanghui triangle.
    /// Return:
    ///     int: minimum path length
    public static int DP1(int[][] jagArray)
    {
        var state = new int[jagArray.Length, jagArray.Length];
        state[0, 0] = jagArray[0][0];
        for (int i = 1; i < jagArray.Length; i++)
        {
            for (int j = 0; j < jagArray[i].Length; j++)
            {
                if (j == 0)
                    state[i, j] = state[i - 1, j] + jagArray[i][j];
                else if (j == i)
                    state[i, j] = state[i - 1, j - 1] + jagArray[i][j];
                else
                {
                    int topLeft = state[i - 1, j - 1];
                    int topRight = state[i - 1, j];
                    state[i, j] = Math.Min(topLeft, topLeft) + jagArray[i][j];
                }
            }
        }

        int minLen = int.MaxValue;
        for (int j = 0; j < jagArray.Length; j++)
        {
            if (minLen > state[jagArray.Length - 1, j])
            {
                minLen = state[jagArray.Length - 1, j];
            }
        }

        return minLen;
    }

    public static int DP1(int[,] matrix)
    {
        var len = matrix.GetLength(1);
        var states = new int[len, len];
        states[0, 0] = matrix[0, 0];
        for (int i = 1; i < len; i++)
        {
            for (int j = 0; j < i + 1; j++)
            {
                if (j == 0)
                    states[i, j] = matrix[i, j] + states[i - 1, j];
                else if (j == i)
                    states[i, j] = matrix[i, j] + states[i - 1, j - 1];
                else
                    states[i, j] = matrix[i, j] + Math.Min(states[i - 1, j - 1], states[i - 1, j]);
            }
        }

        var minLen = states[len - 1, 0];
        for (int i = 1; i < len; i++)
        {
            if (minLen > states[len - 1, i])
                minLen = states[len - 1, i];
        }

        return minLen;
    }

    /// <summary>
    /// dynamic programming solution 2
    /// </summary>
    /// Param:
    ///   matrix: yanghui triangle.
    /// Return:
    ///     int: minimum path length
    public static int DP2(int[][] jagArray)
    {
        int len = jagArray.Length;
        int[] state = jagArray[len - 1];
        for (int i = len - 2; i >= 0; i--)
        {
            for (int j = 0; j < jagArray[i].Length; j++)
            {
                state[j] = Math.Min(state[j], state[j + 1]) + jagArray[i][j];
            }
        }
        return state[0];
    }

    public static int DP2(int[,] matrix)
    {
        int len = matrix.GetLength(0);
        var states = new int[len];
        for (int i = 0; i < len; i++)
        {
            states[i] = matrix[len - 1, i];
        }

        for (int i = len - 2; i >= 0; i--)
        {
            for (int j = 0; j < i + 1; j++)
            {
                states[j] = Math.Min(states[j], states[j + 1]) + matrix[i, j];
            }
        }
        return states[0];
    }
}