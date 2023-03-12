namespace Advanced;

/// <summary>
/// A* - Heuristically Search Algorithm
/// GameMap is Directed Weight Graph
/// </summary>
public class GameMap
{
    private LinkedList<Edge>[] _adj; //adjacency list
    private int _v; // vertex number
    private Vertex[] _vertexes;

    public GameMap(int v)
    {
        _v = v;
        _vertexes = new Vertex[v];
        _adj = new LinkedList<Edge>[v];
        for (int i = 0; i < v; i++)
        {
            _adj[i] = new LinkedList<Edge>();
        }
    }

    public void AddVertex(int id, int x, int y)
    {
        _vertexes[id] = new Vertex(id, x, y);
    }

    public void AddEdge(int s, int t, int w)
    {
        _adj[s].AddLast(new Edge(s, t, w));
    }

    private class Vertex
    {
        public int id;
        public int dist; // g(i), distance of start -> current
        public int f; // f(i) = g(i) + h(i)
        public int x, y; // coordinates in map
        public Vertex(int id, int x, int y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.f = int.MaxValue;
            this.dist = int.MaxValue;
        }
    }

    /// <summary>
    /// Directed Weight Edge
    /// </summary>
    private class Edge
    {
        public int sid; // edge's start vertex
        public int tid; // edge's target vertex
        public int w; //weight

        public Edge(int s, int t, int w)
        {
            sid = s;
            tid = t;
            this.w = w;
        }
    }

    private int hManhattan(Vertex v1, Vertex v2)
    {
        return Math.Abs(v1.x - v2.x) + Math.Abs(v1.y - v2.y);
    }

    /// <summary>
    /// the path of s -> t.
    /// </summary>
    public void AStar(int s, int t)
    {
        var predecessor = new int[_v]; // used to restore path
        var queue = new PriorityQueue(_v); // build min heap based on the f value of Vertex
        var inqueue = new bool[_v]; // whether entered queue
        _vertexes[s].dist = 0;
        _vertexes[s].f = 0;
        queue.Enqueue(_vertexes[s]);
        inqueue[s] = true;

        while (!queue.IsEmpty())
        {
            var minVertex = queue.Dequeue();
            for (int i = 0; i < _adj[minVertex.id].Count; i++)
            {
                var e = _adj[minVertex.id].ElementAt(i); // get a edge linked with minVertex
                var nextVertex = _vertexes[e.tid]; // minVertex --> nextVertex
                if (minVertex.dist + e.w < nextVertex.dist) // update dist, f of next
                {
                    nextVertex.dist = minVertex.dist + e.w;
                    nextVertex.f = nextVertex.dist + hManhattan(nextVertex, _vertexes[t]);
                    predecessor[nextVertex.id] = minVertex.id;
                    if (inqueue[nextVertex.id] == true)
                    {
                        queue.Update(nextVertex);
                    }
                    else
                    {
                        queue.Enqueue(nextVertex);
                        inqueue[nextVertex.id] = true;
                    }
                }

                // exit while loop when reach t
                if (nextVertex.id == t)
                {
                    // clear queue before exit
                    queue.Clear();
                    break;
                }
            }
        }

        //export path
        Console.Write(s);
        Print(s, t, predecessor);
    }

    private void Print(int s, int t, int[] predecessor)
    {
        if (s == t) return;

        Print(s, predecessor[t], predecessor);
        Console.Write($"->{t}");
    }

    /// <summary>
    /// Customized priority queue based on Min Heap of f.
    /// </summary>
    private class PriorityQueue
    {
        private Vertex[] _nodes;
        private int _count;

        public PriorityQueue(int v)
        {
            _nodes = new Vertex[v + 1];
            _count = 0;
        }

        public void Clear()
        {
            _count = 0;
        }

        public Vertex? Dequeue()
        {
            if (_count == 0)
                return null;

            var ret = _nodes[1];
            _nodes[1] = _nodes[_count];
            --_count;
            TopHeapify(_count, 1);

            return ret;
        }

        public void Enqueue(Vertex vertex)
        {
            if (_count >= _nodes.Length - 1) //-1
                return;

            ++_count;
            _nodes[_count] = vertex;
            int i = _count;

            BottomHeapify(i);
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        public void Update(Vertex vertex)
        {
            for (int i = 1; i <= _count; i++)
            {
                if (_nodes[i].id == vertex.id)
                {
                    _nodes[i].dist = vertex.dist;
                    _nodes[i].f = vertex.f;
                    BottomHeapify(i);
                }
            }
        }

        /// <summary>
        /// Heapify from bottom to top
        /// </summary>
        private void BottomHeapify(int i)
        {
            while (i / 2 > 0 && _nodes[i].f < _nodes[i / 2].f)
            {
                Swap(i, i / 2);
                i = i / 2;
            }
        }

        private void TopHeapify(int n, int i)
        {
            int minPos = i;
            while (true)
            {
                if (i * 2 <= n && _nodes[i].f > _nodes[i * 2].f)
                    minPos = i * 2;
                if (i * 2 + 1 <= n && _nodes[minPos].f > _nodes[i * 2 + 1].f)
                    minPos = i * 2 + 1;
                if (minPos == i)
                    break;

                Swap(i, minPos);
                i = minPos;
            }
        }

        private void Swap(int i, int j)
        {
            var tempV = _nodes[i];
            _nodes[i] = _nodes[j];
            _nodes[j] = tempV;
        }
    }
}