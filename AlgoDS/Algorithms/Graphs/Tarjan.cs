using System;
using System.Collections.Generic;

namespace AlgoDS.Algorithms.Graphs
{
    public class Tarjan
    {
        //Low-link value: for each node u, low-link value is defined as lowest id(IN time) of node reachable from node u.
        //During execution of DFS and calculation of low-link value, Tarjan's algorithm keeps track of "active" nodes.
        //A node u can use node v to minimize it's low-link value if and only if v is in "active" nodes list.

        const int V = 1001;
        int n;
        List<int>[] adj = new List<int>[V + 1];
        int[] inTime = new int[V + 1];
        int[] lowLink = new int[V + 1];
        bool[] vis = new bool[V + 1];
        bool[] onStack = new bool[V + 1];
        Stack<int> stack = new Stack<int>();
        int timer = 1;
        int scc = 0;

        public Tarjan(int nodes)
        {
            n = nodes;

            for (int i = 0; i <= V; i++)
                adj[i] = new List<int>();
        }

        void AddEdge(int a, int b)
        {
            adj[a].Add(b);
        }

        void Dfs(int node)
        {
            vis[node] = true;
            stack.Push(node);
            onStack[node] = true;
            inTime[node] = lowLink[node] = timer++;

            foreach (var child in adj[node])
            {
                if (vis[child] && onStack[child])
                {
                    lowLink[node] = Math.Min(lowLink[node], inTime[child]);
                }
                else if (!vis[child])
                {
                    Dfs(child);

                    if (onStack[child])
                        lowLink[node] = Math.Min(lowLink[node], lowLink[child]);
                }
            }

            if (inTime[node] == lowLink[node])
            {
                scc++;
                Console.WriteLine("SCC #" + scc);
                while (true)
                {
                    int u = stack.Pop();
                    Console.Write(u + " ");
                    onStack[u] = false;

                    if (node == u)
                        break;
                }
                Console.WriteLine();
            }
        }

        public void FindSCC()
        {
            for (int i = 1; i <= n; i++)
            {
                if (!vis[i])
                    Dfs(i);
            }
        }
    }
}

/*
8 12
1 2
2 3
3 1
2 8
3 4
8 4
4 8
7 8
5 4
5 7
7 6
6 5
 */