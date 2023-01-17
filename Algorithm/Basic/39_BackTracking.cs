namespace Basic;

/** 
* @Desc: N Queens problem. 
*/
public class NQueensProblem
{
    int[] Result { get; set; }
    int Number { set; get; } // the number of Queens
    public int Total { set; get; } = 0; // the number of setup

    public NQueensProblem(int num)
    {
        Number = num;
        Result = new int[num];
    }

    /// Summary: recursive to find out all sets. Call CalNQueens(0)
    public void CalNQueens(int row)
    {
        if (row == Number) // all queens are setup, output result.
        {
            PrintQueens(Result);
            Total++;
            return;
        }

        for (int column = 0; column < Number; column++)
        {
            if (IsOk(row, column))
            {
                Result[row] = column; // place queue to [row, column]
                CalNQueens(row + 1);
            }
        }
    }

    /// Summary: recursive to find out all sets. Call CalNQueens(0)
    ///
    private bool IsOk(int row, int column)
    {
        int leftup = column - 1;
        int rightup = column + 1;
        for (int i = row - 1; i >= 0; i--)// 逐行往上检查在[row, column]放置棋子是否合适
        {
            if (Result[i] == column)// 中轴线上有棋子
                return false;

            if (leftup >= 0)
            {
                if (Result[i] == leftup)//左对角线有棋子
                    return false;
            }

            if (rightup < 8)
            {
                if (Result[i] == rightup)// //左对角线有棋子
                    return false;
            }
            --leftup;
            ++rightup;
        }
        return true;
    }

    /// Summary: print two-dimension matrix
    ///
    private void PrintQueens(int[] result)
    {
        for (int row = 0; row < result.Length; row++)
        {
            for (int column = 0; column < result.Length; column++)
            {
                if (result[row] == column)
                    Console.Write("Q ");
                else
                    Console.Write("* ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}

/** 
* @Desc: Using back tracking to resolve 0-1 knapsack Problem 
*/
public class KnapsackProblem
{
    public int MaxW = int.MinValue;// Maximum value of weight of items in the bag.

    /// Summary: 0-1 knapsack algorithm
    ///
    /// Param:
    ///   i: index of seachred items
    ///   cw: current weight of items in the bag
    ///   items: array of weight of goods
    ///   n: number of items
    ///   w: the load weight limit of the bag
    ///
    /// Example:
    ///     f(0, 0, a, 10, 100)
    public void f(int i, int cw, int[] items, int n, int w)
    {
        // cw==w, bag is full; 
        // i==n, all items are searched.
        if (cw == w || i == n)
        {
            if (cw > MaxW)
                MaxW = cw;
            return;
        }

        f(i + 1, cw, items, n, w); // don't add i into bag

        if (cw + items[i] <= w) //(剪枝操作) stop searching if current weight greater than weight limit 
            f(i + 1, cw + items[i], items, n, w); // add i into bag
    }
}

/** 
* @Desc: Using back tracking to implement regex match 
*/
public class Pattern
{
    private bool Matched { get; set; } = false;
    private char[] _pattern; // regex expression
    private int _pLen;

    public Pattern(string pattern)
    {
        this._pattern = pattern.ToCharArray();
        this._pLen = pattern.Length;
    }

    public bool Match(string text)
    {
        Matched = false;
        rmatch(0, 0, text.ToCharArray(), text.Length);
        return Matched;
    }

    private void rmatch(int ti, int pj, char[] text, int tLen)
    {
        if (Matched)
            return;

        if (pj == _pLen) // pattern ends
        {
            if (ti == tLen) // text ends
                Matched = true;
            return;
        }

        if (_pattern[pj] == '*')// * match any character any times
        {
            for (int k = 0; k < tLen - ti; k++)
            {
                rmatch(ti + k, pj + 1, text, tLen);
            }
        }
        else if (_pattern[pj] == '?') // ? match any character 0 or 1 time
        {
            rmatch(ti, pj + 1, text, tLen);
            rmatch(ti + 1, pj + 1, text, tLen);
        }
        else if (ti < tLen && _pattern[pj] == text[ti]) // match characters
        {
            rmatch(ti + 1, pj + 1, text, tLen);
        }
    }
}