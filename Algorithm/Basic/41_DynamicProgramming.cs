namespace Basic;

/// <summary>
/// Question: 2D Array w[n,n] and the element stores path. Assume the left-up cornor is the start, the right-botton is the end.
/// Caculate the shortest path from start to end.
/// <summary>
public class MatrixDistance
{
    public int MinDist { get; set; } = int.MaxValue;

    /// <summary>
    /// back tracking solution
    /// </summary>
    /// Param:
    ///   i: row
    ///   j: column
    ///   dist: distance
    ///   w: Matrix
    ///   n: the number of row or column
    public void MinBT(int i, int j, int dist, int[,] matrix, int n)
    {
        dist += matrix[i, j];
        if (i == n - 1 && j == n - 1)
        {
            if (MinDist > dist)
                MinDist = dist;
            return;
        }

        if (i < n - 1)// down, i=i+1, j=j
            MinBT(i + 1, j, dist, matrix, n);

        if (j < n - 1)// right, i=i, j=j+1
            MinBT(i, j + 1, dist, matrix, n);
    }

    /// <summary>
    /// status transfer table of Dynamic programming
    /// </summary>
    /// Param:
    ///   w: Matrix
    ///   n: the number of row or column
    public int MinDP(int[,] matrix, int n)
    {
        int[,] states = new int[n, n];
        int sum = 0;

        for (int j = 0; j < n; j++) // 初始化第一行
        {
            sum += matrix[0, j];
            states[0, j] = sum;
        }
        sum = 0;
        for (int i = 0; i < n; i++)// 初始化第一列
        {
            sum += matrix[i, 0];
            states[i, 0] = sum;
        }

        for (int i = 1; i < n; i++)
        {
            for (int j = 1; j < n; j++)
            {
                states[i, j] = matrix[i, j] + Math.Min(states[i - 1, j], states[i, j - 1]);
            }
        }

        return states[n - 1, n - 1];
    }

    /// <summary>
    /// status transfer expression of Dynamic programming
    /// min_dist(i, j) = w[i][j] + min(min_dist(i, j-1), min_dist(i-1, j))
    /// </summary>
    /// Param:
    ///   i: row
    ///   j: column
    ///   dist: distance
    ///   w: Matrix
    ///   n: the number of row or column
    private int[,] _mem = new int[4, 4];
    public int MinDP1(int i, int j, int[,] matrix)
    {
        if (i == 0 && j == 0)
            return matrix[0, 0];
        if (_mem[i, j] > 0)
            return _mem[i, j];

        int minLeft = int.MaxValue;
        if (j - 1 >= 0)
            minLeft = MinDP1(i, j - 1, matrix);

        int minUp = int.MaxValue;
        if (i - 1 >= 0)
            minUp = MinDP1(i - 1, j, matrix);

        int curMinDist = matrix[i, j] + Math.Min(minLeft, minUp);
        _mem[i, j] = curMinDist;
        return curMinDist;
    }
}

/// <summary>
/// Exercise 41: calcuate the minimum number of coins need to pay 'w' yuan
/// </summary>
public class CoinNumber
{
    public int MinNum { get; set; } = int.MaxValue;

    public void BT(int w, int num, int[] coins)
    {
        if (w == 0)
        {
            if (num > 0 && MinNum > num)
                MinNum = num;
            return;
        }
        else if (w < 0)
            return;

        for (int i = 0; i < coins.Length; i++)
        {
            BT(w - coins[i], num + 1, coins);
        }
    }

    /// <summary>
    /// Dynamic Programming - status transferred formula
    /// f(9) = 1 + min(f(8), f(6), f(4))
    /// </summary>
    /// coin array 1, 3, 5
    /// Param:
    /// w: money
    public static int DP(int w)
    {
        //w-coin>5
        if (w == 5 || w == 3)
            return 1;
        else if (w > 0 && w < 3)
            return w;
        else if (w < 0)
            return int.MaxValue; //abandon result

        int f5 = DP(w - 5);
        int f3 = DP(w - 3);
        int f1 = DP(w - 1);

        int num = Math.Min(f5, f3);
        num = Math.Min(num, f1);

        return num + 1;
    }

    /// <summary>
    /// Dynamic Programming - status transferred table
    /// </summary>
    /// Param:
    ///     w: money
    public static int DP1(int w)
    {
        if (w == 1 || w == 3 || w == 5)
        {
            return 1;
        }

        var states = new bool[w, w + 1];
        if (w > 1) states[0, 1] = true;
        if (w > 3) states[0, 3] = true;
        if (w > 5) states[0, 5] = true;

        for (int i = 1; i < w; i++)
        {
            for (int j = 0; j <= w; j++)
            {
                if (states[i - 1, j] == true)
                {
                    if (j + 1 <= w) states[i, j + 1] = true;
                    if (j + 3 <= w) states[i, j + 3] = true;
                    if (j + 5 <= w) states[i, j + 5] = true;

                    if (states[i, w])
                        return i + 1;
                }
            }
        }
        return w;
    }
}