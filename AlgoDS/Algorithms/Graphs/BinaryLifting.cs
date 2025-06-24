using System;
using System.Collections.Generic;

namespace AlgoDS.Algorithms.Graphs
{
    public class BinaryLifting
    {
        int V;

        List<int>[] adj;

        int LOG; //log2(N)

        int[][] up; //up[i][j] - (2 ^ j)-th parent of i

        int[] depth;

        public BinaryLifting(int v)
        {
            V = v;
            adj = new List<int>[V + 1];

            for (int i = 0; i <= V; i++)
            {
                adj[i] = new List<int>();
            }

            LOG = (int)Math.Log2(V);

            up = new int[V + 1][];

            for (int i = 1; i <= V; i++)
            {
                up[i] = new int[LOG + 1];
                for (int j = 0; j <= LOG; j++)
                {
                    up[i][j] = -1;
                }
            }

            depth = new int[V + 1];
        }

        public void AddEdge(int a, int b)
        {
            if (a >= adj.Length || b >= adj.Length)
                return;

            adj[a].Add(b);
            adj[b].Add(a);
        }

        //O(log(n))
        public int LCA(int a, int b)
        {
            if (depth[a] > depth[b])
                Swap(ref a, ref b);

            int d = depth[b] - depth[a];

            while (d > 0)
            {
                int i = (int)Math.Log2(d);
                b = up[b][i];
                d -= (1 << i);
            }

            if (a == b)
                return a;

            for (int i = LOG; i >= 0; i--)
            {
                if (up[a][i] != -1 && (up[a][i] != up[b][i]))
                {
                    a = up[a][i];
                    b = up[b][i];
                }
            }

            return up[a][0];
        }

        //O(log(n)), if node is zero-based
        int KthAncestor(int node, int k)
        {
            node++;
            if (depth[node] < k)
                return -1;

            for (int j = 0; j <= LOG; j++)
            {
                if ((k & (1 << j)) > 0)
                    node = up[node][j];
            }

            return node - 1;
        }

        //O(log(n))
        public int GetDistance(int a, int b)
        {
            int lca = LCA(a, b);
            return depth[a] + depth[b] - 2 * depth[lca];
        }

        //O(n)
        void FillZerothParent(int node, int parent, int level)
        {
            up[node][0] = parent;
            depth[node] = level;

            foreach (int child in adj[node])
            {
                if (child != parent)
                    FillZerothParent(child, node, level + 1);
            }
        }

        //O(nlog(n))
        public void Preprocess()
        {
            FillZerothParent(1, -1, 0);

            for (int j = 1; j <= LOG; j++)
            {
                for (int i = 1; i <= V; i++)
                {
                    if (up[i][j - 1] != -1)
                    {
                        up[i][j] = up[up[i][j - 1]][j - 1];
                    }
                }
            }
        }

        void Swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }
    }
}
