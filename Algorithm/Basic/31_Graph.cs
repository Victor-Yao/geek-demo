namespace Basic;

/// <summary>
/// Undirected graph with BFS and DFS algorithm 
/// </summary>
public class Graph
{
    private int _v; //number of vertex
    private LinkedList<int>[] _adj; // adjency list
    public Graph(int v)
    {
        _v = v;
        _adj = new LinkedList<int>[v];
        for (int i = 0; i < v; i++)
        {
            _adj[i] = new LinkedList<int>();
        }
    }

    /// <summary>
    /// Add vertex to undirected graph
    /// </summary>
    /// Param:
    ///   s, t: vertex
    public void AddEdge(int s, int t)
    {
        if (!_adj[s].Contains(t))
            _adj[s].AddLast(t);

        if (!_adj[t].Contains(s))
            _adj[t].AddLast(s);
    }

    /// <summary>
    /// Breadth-First search a path from 's' to 't'
    /// </summary>
    /// Param:
    ///   s: start vertex
    ///   t: end vertex
    public void BFS(int s, int t)
    {
        if (s == t)
            return;

        var visited = new bool[_v];
        visited[s] = true;
        var queue = new Queue<int>();
        queue.Enqueue(s);
        var prev = new int[_v];
        for (int i = 0; i < _v; i++)
        {
            prev[i] = -1;
        }

        while (queue.Count != 0)
        {
            int w = queue.Dequeue();
            for (int i = 0; i < _adj[w].Count; i++)
            {
                int q = _adj[w].ElementAt<int>(i);
                if (!visited[q])
                {
                    prev[q] = w;
                    if (q == t)
                    {
                        Print(prev, s, t);
                        return;
                    }
                    visited[q] = true;
                    queue.Enqueue(q);
                }
            }
        }
    }

    /// <summary>
    /// recursive print the path s -> t
    /// </summary>
    private void Print(int[] prev, int s, int t)
    {
        if (prev[t] != -1 && t != s)
        {
            Print(prev, s, prev[t]);
        }
        Console.WriteLine(t + "");
    }

    private bool _found = false;
    /// <summary>
    /// Depth-First search a path from 's' to 't'
    /// </summary>
    /// Param:
    ///   s: start vertex
    ///   t: end vertex
    public void DFS(int s, int t)
    {
        _found = false;
        var visited = new bool[_v];
        var prev = new int[_v];
        for (int i = 0; i < _v; i++)
        {
            prev[i] = -1;
        }

        RecurDFS(s, t, visited, prev);
        Print(prev, s, t);
    }

    private void RecurDFS(int w, int t, bool[] visited, int[] prev)
    {
        if (_found == true)
            return;

        visited[w] = true;
        if (w == t)
        {
            _found = true;
            return;
        }

        for (int i = 0; i < _adj[w].Count; i++)
        {
            var q = _adj[w].ElementAt(i);
            if (!visited[q])
            {
                prev[q] = w;
                RecurDFS(q, t, visited, prev);
            }
        }
    }
}