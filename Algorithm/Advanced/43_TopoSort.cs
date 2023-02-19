using System;
using System.Collections.Generic;

namespace Advanced;

/// <summary>
/// Directed acyclic graph
/// </summary>
public class DirectedGraph
{
    private int _v; //vertex
    private LinkedList<int>[] _adj; //adjacency list

    public DirectedGraph(int v)
    {
        this._v = v;
        _adj = new LinkedList<int>[v];
        for (int i = 0; i < v; i++)
        {
            _adj[i] = new LinkedList<int>();
        }
    }

    public void AddEdge(int s, int t)
    {
        _adj[s].AddLast(t); // directed edge s->t
    }

    public void TopSortbyKahn()
    {
        int[] inDegree = new int[_v];
        for (int i = 0; i < _v; i++)
        {
            for (int j = 0; j < _adj[i].Count; j++)
            {
                int w = _adj[i].ElementAt(j); // i -> w
                inDegree[w]++;
            }
        }

        var queue = new Queue<int>();
        for (int i = 0; i < _v; i++)
        {
            if (inDegree[i] == 0)
                queue.Enqueue(i);
        }

        while (queue.Count != 0)
        {
            int i = queue.Dequeue();
            Console.WriteLine("->" + i);

            for (int j = 0; j < _adj[i].Count; j++)
            {
                int k = _adj[i].ElementAt(j);
                inDegree[k]--;
                if (inDegree[k] == 0)
                    queue.Enqueue(k);
            }
        }
    }

    public void TopSortbyDFS()
    {
        var inversAdj = new LinkedList<int>[_v];
        for (int i = 0; i < _v; i++)
        {
            inversAdj[i] = new LinkedList<int>();
        }

        for (int i = 0; i < _v; i++)
        {
            for (int j = 0; j < _adj[i].Count; j++)
            {
                int w = _adj[i].ElementAt(j); // i->w
                inversAdj[w].AddLast(i); // w->i 
            }
        }

        var visited = new bool[_v];
        for (int i = 0; i < _v; i++)
        {
            if (!visited[i])
            {
                visited[i] = true;
                DFS(i, inversAdj, visited);
            }
        }
    }

    private void DFS(int vertex, LinkedList<int>[] inversAdj, bool[] visited)
    {
        for (int i = 0; i < inversAdj[vertex].Count; i++)
        {
            int w = inversAdj[vertex].ElementAt(i);
            if (visited[w])
                continue;
            visited[w] = true;
            DFS(w, inversAdj, visited);
        } // print all depencied vertexs and then print itself 
        Console.WriteLine("->" + vertex);
    }
}