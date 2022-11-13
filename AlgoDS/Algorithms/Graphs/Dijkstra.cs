using System.Collections.Generic;

namespace AlgoDS.Algorithms
{
    public class Dijkstra
    {
        //O((E+V)logV)
        public int ShortestPath(List<(int node, int weight)>[] adj, int src, int dst)
        {
            int v = adj.Length;
            int[] dist = new int[v];
            for (int i = 0; i < v; i++)
                dist[i] = int.MaxValue;
            dist[src] = 0;

            var pq = new PriorityQueue<int, int>();
            pq.Enqueue(src, 0);
            while(pq.Count > 0)
            {
                var u = pq.Dequeue();
                foreach (var ngb in adj[u])
                {
                    if (dist[u] + ngb.weight < dist[ngb.node])
                    {
                        dist[ngb.node] = dist[u] + ngb.weight;
                        pq.Enqueue(ngb.node, dist[ngb.node]);
                    }
                }
            }
            return dist[dst];
        }
    }
}
