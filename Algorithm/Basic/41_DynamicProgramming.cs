using Basic;

/** 
* @Desc: Dynamic Programming in depth. 
*/
public class Dp1
{
    /** 
    * @Question: there is 2D Matix w[n,n], the element stores path. 
    When left-up cornor is the start, right-botton is the end. What's the shortest distance from start to end.
    */
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
    public void MinDistBT(int i, int j, int dist, int[,] matrix, int n)
    {
        dist += matrix[i, j];
        if (i == n - 1 && j == n - 1)
        {
            if (MinDist > dist)
                MinDist = dist;
            return;
        }

        if (i < n - 1)// down, i=i+1, j=j
            MinDistBT(i + 1, j, dist, matrix, n);

        if (j < n - 1)// right, i=i, j=j+1
            MinDistBT(i, j + 1, dist, matrix, n);
    }

    /// <summary>
    /// status transfer table of Dynamic programming
    /// </summary>
    /// Param:
    ///   w: Matrix
    ///   n: the number of row or column
    public int MinDistDP(int[,] matrix, int n)
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
    public int MinDistDP1(int i, int j, int[,] matrix)
    {
        if (i == 0 && j == 0)
            return matrix[0, 0];
        if (_mem[i, j] > 0)
            return _mem[i, j];

        int minLeft = int.MaxValue;
        if (j - 1 >= 0)
            minLeft = MinDistDP1(i, j - 1, matrix);

        int minUp = int.MaxValue;
        if (i - 1 >= 0)
            minUp = MinDistDP1(i - 1, j, matrix);

        int curMinDist = matrix[i, j] + Math.Min(minLeft, minUp);
        _mem[i, j] = curMinDist;
        return curMinDist;
    }
}