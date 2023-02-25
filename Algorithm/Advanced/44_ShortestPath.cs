namespace Advanced;

/// <summary>
/// Dijkstra - Shortest Path Algorithm
/// Directed Weight Graph
/// </summary>
public class DirectedWeightGraph
{
    private LinkedList<Edge>[] _adj; //adjacency list
    private int _v; // vertex number

    public DirectedWeightGraph(int v)
    {
        _v = v;
        _adj = new LinkedList<Edge>[v];
        for (int i = 0; i < v; i++)
        {
            _adj[i] = new LinkedList<Edge>();
        }
    }


    public void AddEdge(int s, int t, int w)
    {
        _adj[s].AddLast(new Edge(s, t, w));
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

    /// <summary>
    /// Vertex with distance from s
    /// </summary>
    private class Vertex
    {
        public int id;
        public int dist; // the distance from start vertex to this one
        public Vertex(int id, int dist)
        {
            this.id = id;
            this.dist = dist;
        }
    }

    /// <summary>
    /// Customized priority queue based on Min Heap.
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

        public void Update(Vertex vertex)
        {
            for (int i = 1; i <= _count; i++)
            {
                if (_nodes[i].id == vertex.id)
                {
                    _nodes[i].dist = vertex.dist;
                    BottomHeapify(i);
                }
            }
        }

        /// <summary>
        /// Heapify from bottom to top
        /// </summary>
        private void BottomHeapify(int i)
        {
            while (i / 2 > 0 && _nodes[i].dist < _nodes[i / 2].dist)
            {
                Swap(i, i / 2);
                i = i / 2;
            }
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        /// <summary>
        /// Heapify from top to bottom
        /// </summary>
        private void TopHeapify(int n, int i)
        {
            int minPos = i;
            while (true)
            {
                if (i * 2 <= n && _nodes[i].dist > _nodes[i * 2].dist)
                    minPos = i * 2;
                if (i * 2 + 1 <= n && _nodes[minPos].dist > _nodes[i * 2 + 1].dist)
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

    /// <summary>
    /// work out the shortest path from s to t.
    /// </summary>
    public void Dijkstra(int s, int t)
    {
        var predecessor = new int[_v]; // store the shortest path
        var vertexes = new Vertex[_v];
        for (int i = 0; i < _v; i++)
        {
            vertexes[i] = new Vertex(i, int.MaxValue);
        }

        var queue = new PriorityQueue(_v);//Min heap
        var inqueue = new bool[_v];//enter queued already?
        vertexes[s].dist = 0;
        queue.Enqueue(vertexes[s]);
        inqueue[s] = true;
        while (!queue.IsEmpty())
        {
            var minVertex = queue.Dequeue(); //Get and remove Heap top element

            if (minVertex.id == t) // found shorttest path
                break;

            for (int i = 0; i < _adj[minVertex.id].Count; i++)
            {
                var edge = _adj[minVertex.id].ElementAt(i); // 取出一条minVetex相连的边
                Vertex nextVertex = vertexes[edge.tid]; // minVertex-->nextVertex
                if (minVertex.dist + edge.w < nextVertex.dist)
                { 
                    nextVertex.dist = minVertex.dist + edge.w; // update next.dist
                    predecessor[nextVertex.id] = minVertex.id;
                    if (inqueue[nextVertex.id])
                        queue.Update(nextVertex); // update dist value in the queue
                    else
                    {
                        queue.Enqueue(nextVertex);
                        inqueue[nextVertex.id] = true;
                    }
                }
            }
        }

        Console.Write($"Distance: {vertexes[t].dist}; Path: {s}"); // output shorttest path
        Print(s, t, predecessor);
    }

    private void Print(int s, int t, int[] predecessor)
    {
        if (s == t) return;

        Print(s, predecessor[t], predecessor);
        Console.Write($"->{t}");
    }
}