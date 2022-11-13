using System.Collections.Generic;

namespace AlgoDS.Algorithms
{
    public class Prim
    {
        //O((E+V)log(V))
        public int MinimumSpanningTree(List<(int node, int weight)>[] adj, int src)
        {
            int v = adj.Length;
            int[] dist = new int[v];
            for (int i = 0; i < v; i++)
                dist[i] = int.MaxValue;
            dist[src] = 0;

            int[] parent = new int[v];
            for (int i = 0; i < v; i++)
                parent[i] = -1;

            bool[] added = new bool[v];

            var pq = new PriorityQueue<int, int>();
            pq.Enqueue(src, 0);            
            while (pq.Count > 0)
            {
                int u = pq.Dequeue();

                if (added[u])
                    continue;

                added[u] = true;

                foreach (var ngb in adj[u])
                {
                    if (!added[ngb.node] && dist[ngb.node] > ngb.weight)
                    {
                        dist[ngb.node] = ngb.weight;
                        pq.Enqueue(ngb.node, dist[ngb.node]);
                        parent[ngb.node] = u;
                    }
                }
            }

            int sum = 0;
            for (int i = 0; i < v; i++)
            {
                if (dist[i] == int.MaxValue)
                    return -1;
                sum += dist[i];
            }
            return sum;
        }
    }
}
