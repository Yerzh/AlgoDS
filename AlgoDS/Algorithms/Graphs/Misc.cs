using System.Collections.Generic;

namespace AlgoDS.Algorithms.Graphs
{
    public class Misc
    {
        const int V = 100001;
        List<int>[] adj = new List<int>[V + 1];
        bool [] vis = new bool[V + 1];
        int[] color = new int[V + 1];
        
        int[] inTime = new int[V + 1];
        int[] outTime = new int [V + 1];
        int curTime = 1;

        public Misc()
        {
            for (int i = 0; i <= V; i++)
                adj[i] = new List<int>();
        }

        public bool IsBipartite(int v, int c)
        {
            vis[v] = true;
            color[v] = c;

            foreach(var child in adj[v])
            {
                if (!vis[child])
                {
                    if (!IsBipartite(child, c ^ 1))
                        return false;
                }
                else if (color[child] == color[v])
                    return false;
            }

            return true;
        }

        public bool ContainsCycle(int node, int parent = -1)
        {
            vis[node] = true;

            foreach(var child in adj[node])
            {
                if (!vis[child])
                {
                    if (ContainsCycle(child, node))
                        return true;
                }
                else if (parent != child)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Motivation: Given 2 nodes (u, v), find whether one node lies in the subtree of other node
        /// to solve this problem, first we need to precompute in and out time arrays and
        /// check whether inTime[u] < inTime[v] && outTime[u] > outTime[v]
        /// </summary>
        public void InOutTime(int node)
        {
            vis[node] = true;
            inTime[node] = curTime++;

            foreach(var child in adj[node])
            {
                if (!vis[child])
                {
                    InOutTime(child);
                }
            }

            outTime[node] = curTime++;
        }
    }
}

/*
 * Diameter of a tree is defined as the longest path between any 2 nodes in it.
 * It can be found by dfs traversing from any arbitrary node to find the farthest node,
 * let this node be x. Run a dfs from node x and find maximum distance from this node
 * to any other node, this distance is diameter.
 */