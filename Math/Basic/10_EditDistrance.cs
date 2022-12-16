
namespace Basic
{
    
    public static class EditDistance
    {
        /** 
        * @Description: 使用状态转移方程，计算两个字符串之间的编辑距离 
        * @param a-第一个字符串，b-第二个字符串 
        * @return int-两者之间的编辑距离 
        */
        public static int GetStrDistance(string a, string b)
        {
            if (a == null || b == null)
                return -1;

            // 初始用于记录化状态转移的二维表
            int[,] d = new int[a.Length + 1, b.Length + 1];

            // i=0, j>0, d[i, j] = j
            for (int j = 0; j <= b.Length; j++)
            {
                d[0, j] = j;
            }

            //  i=0, j>0, d[i, j] = i
            for (int i = 0; i <= a.Length; i++)
            {
                d[i, 0] = i;
            }

            // 实现状态转移方程 
            // 请注意由于Java语言实现的关系，代码里的状态转移是从d[i, j]到d[i+1, j+1]，而不是从d[i-1, j-1]到d[i, j]。本质上是一样的。
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    int r = 0;
                    if (a.ElementAt(i) != b.ElementAt(j))
                        r = 1;

                    int first_append = d[i, j + 1] + 1;
                    int second_append = d[i + 1, j] + 1;
                    int replace = d[i, j] + r;

                    int min = Math.Min(first_append, second_append);
                    min = Math.Min(min, replace);
                    d[i + 1, j + 1] = min;
                }
            }

            return d[a.Length, b.Length];
        }
    }
}