using Basic;

/** 
* @Desc: Dynamic Programming ABC. 
*/
public class Dp
{
    public int Count { get; set; } = 0;

    /// <summary>
    /// basic 0-1 knapsack problem. 2D array DP solution 
    /// </summary>
    /// Param:
    ///   weight: items weight array
    ///   n: number of items
    ///   w: weight limit
    /// Return:
    ///     maximum weight sum
    public int Knapsack(int[] weight, int n, int w)
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

            for (int j = 0; j < w - weight[i]; j++)// 把第i个物品放入背包
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
    /// basic 0-1 knapsack problem. 1D array DP solution 
    /// </summary>
    /// Param:
    ///   weight: items weight array
    ///   n: number of items
    ///   w: weight limit
    /// Return:
    ///     maximum weight sum
    public int Knapsack2(int[] weight, int n, int w)
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


    /// <summary>
    /// advanced 0-1 knapsack problem.
    /// </summary>
    /// Param:
    ///   weight: items weight array
    ///   values: items weight array
    ///   n: number of items
    ///   w: weight limit
    /// Return:
    ///     maximum weight sum
    public int Knapsack3(int[] weights, int[] values, int n, int w)
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

    /// <summary>
    /// 在满足双11满减活动的条件下，如何从购物车的n个商品中选出的商品总价最小。
    /// </summary>
    /// Param:
    ///   prices: goods price array
    ///   n: number of goods
    ///   w: discount condition
    /// Return:
    ///     maximum weight sum
    public void Double11Question(int[] prices, int n, int w)
    {
        int limit = 3 * w;// 超过满减条件3倍 没有意义
        var states = new bool[n, limit + 1];
        states[0, 0] = true;
        if (prices[0] <= limit)
            states[0, prices[0]] = true;

        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j <= limit; j++)
            {
                if (states[i - 1, j] == true)
                    states[i, j] = states[i - 1, j];
            }

            for (int j = 0; j <= limit - prices[i]; j++)
            {
                if (states[i - 1, j] == true)
                    states[i, j + prices[i]] = true;

            }
        }

        int k;
        for (k = 0; k < limit + 1; k++)
        {
            if (states[n - 1, k] == true)
                break;
        }
        if (k == limit + 1) //无解
            return;

        for (int i = n - 1; i >= 1; i--)
        {
            if (k - prices[i] >= 0 && states[i - 1, k - prices[i]] == true)
            {
                Console.Write(prices[i] + " ");
                k = k - prices[i];
            } // else 没有购买这个商品，k不变。
        }
        if (k != 0)
            Console.Write(prices[0]);
    }
}

// backing trakcing solutions
public class BT
{
    public int MaxV { get; set; } = int.MinValue;
    private int[] _weights;
    private int[] _values;
    private int _n; // total number of items
    private int _w; // weight limit

    public BT(int[] weights, int[] values, int wLimit)
    {
        _weights = weights;
        _values = values;
        _n = weights.Length;
        _w = wLimit;
        _mem = new bool[_n, _w + 1];
    }


    private bool[,] _mem;
    public int MaxW { set; get; } = int.MinValue;
    /// <summary>
    /// basic 0-1 knapsack problem. Back Tracking Mem solution.
    /// </summary>
    /// Param:
    ///   i: the index of item need to be checked weither load or not.
    ///   cw: current weight
    ///   cv: current value
    /// Return:
    ///     nod return
    /// call example:
    ///     f(0,0,0)
    public void f1(int i, int cw)
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

        f1(i + 1, cw); // don't add i
        if (cw + _weights[i] <= _w)
            f1(i + 1, cw + _weights[i]); // add i
    }

    /// <summary>
    /// advanced 0-1 knapsack problem. Back Tracking solution.
    /// </summary>
    /// Param:
    ///   i: the index of item need to be checked weither load or not.
    ///   cw: current weight
    ///   cv: current value
    /// Return:
    ///     nod return
    /// call example:
    ///     f(0,0,0)
    public void f(int i, int cw, int cv)
    {
        if (cw == _w || i == _n)
        {
            if (cv > MaxV)
                MaxV = cv;

            return;
        }

        f(i + 1, cw, cv); // don't add i 

        if (cw + _weights[i] <= _w)
        {
            f(i + 1, cw + _weights[i], cv + _values[i]); // add i
        }
    }
}