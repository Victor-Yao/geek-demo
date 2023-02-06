using Basic;


/// <summary>
/// Edit distance of Dynamic Programming.
/// </summary>
public class Dp2
{
    private char[] _a;
    private char[] _b;
    private int _aNum;
    private int _bNum;
    public int MinDist { get; set; } = int.MaxValue;

    public Dp2(string a, string b)
    {
        _a = a.ToCharArray();
        _aNum = a.Length;
        _b = b.ToCharArray();
        _bNum = b.Length;
    }

    /// <summary>
    /// caculate Levenshtein Distance by Back Tracking.
    /// </summary>
    /// Param:
    ///   i: index of _a
    ///   j: index of _b
    ///   edist: edit distance
    public void LwstBT(int i, int j, int edist)
    {
        if (i == _aNum || j == _bNum)
        {
            if (i < _aNum)
                edist += _aNum - i;
            if (j < _bNum)
                edist += _bNum - j;
            if (edist < MinDist)
                MinDist = edist;
            return;
        }

        if (_a[i] == _b[j])
        {
            LwstBT(i + 1, j + 1, edist);
        }
        else
        {
            LwstBT(i + 1, j, edist + 1); // skip a[i]
            LwstBT(i, j + 1, edist + 1); // skip b[j]
            LwstBT(i + 1, j + 1, edist + 1); // replace a[i] of b[j]
        }
    }

    /// <summary>
    /// caculate Levenshtein Distance by Dynamic Programming
    /// </summary>
    /// Param:
    ///   string a
    ///   string b
    public static int LwstDP(string a, string b)
    {
        int aLen = a.Length;
        int bLen = b.Length;
        var minDist = new int[aLen, bLen];

        for (int j = 0; j < bLen; j++) // initialize row 0
        {
            if (a[0] == b[j])
                minDist[0, j] = j;
            else if (j != 0)
                minDist[0, j] = minDist[0, j - 1] + 1;
            else
                minDist[0, j] = 1;
        }

        for (int i = 0; i < aLen; i++) // initialize column 0
        {
            if (a[i] == b[0])
                minDist[i, 0] = i;
            else if (i != 0)
                minDist[i, 0] = minDist[i - 1, 0] + 1;
            else
                minDist[i, 0] = 1;
        }

        for (int i = 1; i < aLen; i++) // fill in table by row
        {
            for (int j = 1; j < bLen; j++)
            {
                if (a[i] == b[j])
                    minDist[i, j] = Min(minDist[i - 1, j] + 1, minDist[i, j - 1] + 1, minDist[i - 1, j - 1]);
                else
                    minDist[i, j] = Min(minDist[i - 1, j] + 1, minDist[i, j - 1] + 1, minDist[i - 1, j - 1] + 1);
            }
        }

        return minDist[aLen - 1, bLen - 1];
    }

    private static int Min(int x, int y, int z)
    {
        int minv = int.MaxValue;
        if (x < minv) minv = x;
        if (y < minv) minv = y;
        if (z < minv) minv = z;

        return minv;
    }

    /// <summary>
    /// caculate Common Longest Distance by Dynamic Programming
    /// </summary>
    /// Param:
    ///   string a
    ///   string b
    public static int LcsDP(string a, string b)
    {
        int aLen = a.Length;
        int bLen = b.Length;
        var maxLcs = new int[aLen, bLen];

        for (int j = 0; j < bLen; j++) // initialize row 0
        {
            if (a[0] == b[j])
                maxLcs[0, j] = 1;
            else if (j != 0)
                maxLcs[0, j] = maxLcs[0, j - 1];
            else
                maxLcs[0, j] = 0;
        }

        for (int i = 0; i < aLen; i++) // initialize column 0
        {
            if (a[i] == b[0])
                maxLcs[i, 0] = 1;
            else if (i != 0)
                maxLcs[i, 0] = maxLcs[i - 1, 0];
            else
                maxLcs[i, 0] = 0;
        }

        for (int i = 1; i < aLen; i++) // fill in table
        {
            for (int j = 1; j < bLen; j++)
            {
                if (a[i] == b[j])
                    maxLcs[i, j] = Max(maxLcs[i - 1, j], maxLcs[i, j - 1], maxLcs[i - 1, j - 1] + 1);
                else
                    maxLcs[i, j] = Max(maxLcs[i - 1, j], maxLcs[i, j - 1], maxLcs[i - 1, j - 1]);
            }
        }

        return maxLcs[aLen - 1, bLen - 1];
    }

    private static int Max(int x, int y, int z)
    {
        int maxv = int.MinValue;
        if (x > maxv) maxv = x;
        if (y > maxv) maxv = y;
        if (z > maxv) maxv = z;

        return maxv;
    }

    /// <summary>
    /// get the Longest Increasing Subsequence
    /// {2, 9, 3, 6, 5, 1, 7} ==> {2, 3, 5, 7}, len=4
    /// </summary>
    /// Param:
    ///   set: int array
    public static List<int> LongestIS(int[] a)
    {
        var result = new List<int>();

        for (int i = 0; i < a.Length; i++)
        {
            var temp = new List<int>() { };
            int c = i; //loop count
            int k = i;
            int p = c + 1;

            while (c < a.Length - i - 1)
            {
                if (a[k] < a[p])
                {
                    if (!temp.Contains(a[k]))
                        temp.Add(a[k]);

                    if (!temp.Contains(a[p]))
                        temp.Add(a[p]);

                    k = p;
                    p++;
                }
                else
                    p++;

                if (p == a.Length)
                {
                    if (result.Count <= temp.Count)
                        result = temp;

                    temp = new List<int>();
                    c++;
                    k = i;
                    p = c + 1;
                }
            }
        }

        return result;
    }

    public static string Result { get; set; } = string.Empty;
    /// <summary>
    /// Exercise: Back Tracking, get the Longest Increasing Subsequence
    /// {2, 9, 3, 6, 5, 1, 7} ==> {2, 3, 5, 7}, len=4
    /// </summary>
    /// Param:
    ///   set: int array
    public static void LongestISBT(int i, int j, int[] a, string seq)
    {
        // Console.WriteLine($"i: {i}, j: {j}, seq: {seq}");
        if (j == a.Length)
        {
            if (Result.Length < seq.Length)
                Result = seq;
            return;
        }

        LongestISBT(i, j + 1, a, seq);

        if (a[i] < a[j])
        {
            if (!seq.Contains(a[i].ToString()))
                seq += a[i];
            if (!seq.Contains(a[j].ToString()))
                seq += a[j];

            LongestISBT(j, j + 1, a, seq);
        }
    }

    /// <summary>
    /// Exercise: Dynamic Programming, caculate the length of Longest Increasing Subsequence, not the subsequence 
    /// </summary>
    /// Param:
    ///   input array
    /// {2, 9, 3, 6, 5, 1, 7}
    /// {3,2,1,4}
    public static int LongestISDP(int[] array)
    {
        if (array.Length < 2)
            return array.Length;

        var maxLen = new int[array.Length];
        // Array.Fill(lengths, 1);
        maxLen[0] = 1;
        for (int i = 1; i < array.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                Console.WriteLine($"i={i}, j={j}, arr_i[{i}] = {array[i]}, arr_j[{j}] = {array[j]}], max_i[{i}]={maxLen[i]}, max_j[{j}]={maxLen[j]}");
                if (array[i] > array[j])
                    maxLen[i] = Math.Max(maxLen[i], maxLen[j] + 1);
            }
        }

        int ret = 0;
        for (int i = 0; i < maxLen.Length; i++)
        {
            if (ret < maxLen[i])
                ret = maxLen[i];
        }
        return ret;
    }
}