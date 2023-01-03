namespace Basic;

/** 
* @Desc: the string match algorithms of Lesson 32-34
*/
public static class StringMatch
{

    private static readonly int _size = 256; // bm - size of bad character hash table 

    /// <summary>
    /// Boyer-Moore算法
    /// </summary>

    /// Summary: bm - 构建坏字符哈希表
    ///
    /// Param:
    ///   b: 模式串
    ///   m: 模式串长度
    ///   bc: 坏字符哈希表 
    private static void GenerateBC(char[] b, int m, int[] bc)
    {
        for (int i = 0; i < _size; i++)
        {
            bc[i] = -1;//initialize bc
        }

        int ascii = 0;
        for (int i = 0; i < m; i++)
        {
            ascii = (int)b[i];
            bc[ascii] = i;
        }
    }

    /// Summary: bm 算法主体
    ///
    /// Param:
    ///   a: 主串
    ///   b: 模式串
    ///
    /// Returns:
    ///   matched, the head index of matched string in the main string
    ///   mismatched, -1
    public static int Bm(string a, string b)
    {
        int n = a.Length;
        int m = b.Length;

        int[] bc = new int[_size]; //记录模式串中每个字符最后出现的位置
        GenerateBC(b.ToCharArray(), m, bc); //构建坏字符哈希表

        int[] suffix = new int[m];
        bool[] prefix = new bool[m];
        GenerateGS(b.ToCharArray(), m, suffix, prefix);

        int i = 0; //主串与模式串对齐的第一个字符
        while (i <= n - m)
        {
            int j;
            for (j = m - 1; j >= 0; --j) //模式串从后往前匹配
            {
                if (a[i + j] != b[j])
                    break;
            }

            if (j < 0)
            {
                return i; // 匹配成功
            }

            // 发生不匹配，模式串往后滑动
            // 比较坏字符和好后缀各自的往后滑动的步数，取值较大者
            int x = j - bc[(int)a[i + j]];
            int y = 0;
            if (j < m - 1)// 有好后缀
            {
                y = MoveByGS(j, m, suffix, prefix);
            }
            i = i + Math.Max(x, y);
        }

        return -1;// 匹配失败
    }

    /// Summary: bm - 构建模式串的公共后缀子串
    ///
    /// Param:
    ///   b: 模式串
    ///   m: 模式串长度
    ///   suffix: 下标是子串长度，元素是子串的起始下标值
    ///   prefix: 好后缀的后缀子串是否能匹配模式串的前缀子串
    ///
    private static void GenerateGS(char[] b, int m, int[] suffix, bool[] prefix)
    {
        for (int i = 0; i < m; ++i)
        {
            suffix[i] = -1;
            prefix[i] = false;
        }

        for (int i = 0; i < m - 1; ++i)
        {// b[0,i]
            int j = i;
            int k = 0;// 公共后缀子串的长度
            while (j >= 0 && b[j] == b[m - 1 - k]) // 与b[0, m-1]求公共后缀子串
            {
                --j;
                ++k;
                suffix[k] = j + 1;
            }
            if (j == -1)
            {
                prefix[k] = true;
            }
        }
    }

    /// Summary: bm - 计算好后缀规则移动的步数
    ///
    /// Param:
    ///   j: 坏字符对应的模式串中的字符下标
    ///   m: 模式串长度
    ///   suffix: 下标是子串长度，元素是子串的起始下标值
    ///   prefix: 好后缀的后缀子串是否能匹配模式串的前缀子串
    ///
    /// Returns:
    ///   需要移动的步数
    private static int MoveByGS(int j, int m, int[] suffix, bool[] prefix)
    {
        int k = m - 1 - j; //好后缀长度
        if (suffix[k] != -1) //模式串的子后缀匹配好后缀
        {
            return j - suffix[k] + 1;
        }

        //在好后缀的后缀子串b[r, m-1]中，查找能和模式串前缀子串匹配的最长的后缀子串
        for (int r = j + 2; r < m - 1; ++r)
        {
            if (prefix[m - r] == true) //与前缀子串匹配
            {
                return r; // 后移r步
            }
        }

        return m;
    }


    /// <summary>
    /// KMP算法
    /// </summary>

    /// Summary: kmp 算法主体
    ///
    /// Param:
    ///   a: main string
    ///   b: pattern string
    ///
    /// Returns:
    ///   matched, the head index of matched string in the main string
    ///   mismatched, -1
    public static int Kmp(string a, string b)
    {
        int n = a.Length;
        int m = b.Length;
        var next = GetNexts(b);
        int j = 0;

        for (int i = 0; i < n; i++)
        {
            while (j > 0 && a[i] != b[j]) // 发生不匹配
            {
                //计算模式串向后滑动的步数
                j = next[j - 1] + 1;
            }
            if (a[i] == b[j])
            {
                ++j;
            }

            if (j == m)//匹配成功
            {
                return i - m + 1;
            }
        }

        return -1;//匹配失败
    }

    /// Summary: kmp - 求解Next数组 (失效函数)
    ///
    /// Param:
    ///   b: pattern string
    ///
    /// Returns:
    ///   next array
    private static int[] GetNexts(string b)
    {
        int m = b.Length;
        int[] next = new int[m];

        next[0] = -1;
        int k = -1;

        for (int i = 1; i < m; i++)
        {
            while (k != -1 && b[k + 1] != b[i])
            { //mismatch, find the longest sub-string of prefix string
                k = next[k];
            }
            if (b[k + 1] == b[i])
            { // match, compare next char
                k++;
            }

            // save the index of the last char of matched prefix sub-string
            next[i] = k;
        }

        return next;
    }

    /// <summary>
    /// Brute Force
    /// </summary>
    /// Param:
    ///   a: main string
    ///   b: pattern string
    ///
    /// Returns:
    ///   matched, the head index of matched string in the main string
    ///   mismatched, -1
    public static int Bf(string a, string b)
    {
        int n = a.Length;
        int m = b.Length;

        for (int i = 0; i < n - m + 1; i++)
        {
            int j = 0;
            while (j < m && a[i + j] == b[j])
            {
                ++j;
            }

            if (j == m)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Rabin-Karp 算法
    /// </summary>

    /// Param:
    ///   a: 主串
    ///   b: 模式串
    ///
    /// Returns:
    ///   matched, the head index of matched string in the main string
    ///   mismatched, -1
    public static int Rk(string a, string b)
    {
        int n = a.Length;
        int m = b.Length;

        // save the power of 26
        var powers = new double[m];
        for (int i = 0; i < m; i++)
        {
            powers[i] = Math.Pow(26, i);
        }

        var hashTable = MainStrHashArray(a.ToCharArray(), n, m, powers);

        double b_hash = 0;
        for (int i = 0; i < m; i++)
        {
            b_hash += ((int)b[i] - 97) * powers[m - i - 1];
        }

        for (int i = 0; i < hashTable.Length; i++)
        {
            if (hashTable[i] == b_hash)
                return i;
        }

        return -1;
    }

    /// Summary: RK - 求解主串各个子串的hash表
    ///
    /// Param:
    ///   a: 主串
    ///   n: 主串长度
    ///   m: 模式串长度
    ///
    /// Returns:
    ///   next array
    private static double[] MainStrHashArray(char[] a, int n, int m, double[] powers)
    {
        // a=97 ... z=122
        var h = new double[n - m + 1];

        for (int i = 0; i < n - m + 1; i++)
        {
            int j = 0;
            double sum = 0;
            if (i == 0)
            {
                while (j != m)
                {
                    sum += ((int)a[i + j] - 97) * powers[m - j - 1];
                    ++j;
                }
                h[i] = sum;
            }
            else
            {
                h[i] = 26 * (h[i - 1] - powers[m - 1] * ((int)a[i - 1] - 97)) + ((int)a[i + m - 1] - 97);
            }
        }

        return h;
    }


}
