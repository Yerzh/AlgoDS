using System;
using System.Collections.Generic;

namespace AlgoDS.Algorithms.Graphs
{
    public class Kosaraju
    {
        //Notions:
        //1. SCCs can be found only for directed graph 
        //2. Graph and Transposed graph have the same Strongly Connected Components (SCC).
        //3. Condensation graph is acyclic.
        //4. out[C_i] > out[c_j] if there is an edge from C_i to C_j in condensation graph.

        //Summary: Run Dfs on the graph and assign OUT time of each node, then sort the list by OUT time of nodes

        const int V = 1001;
        List<int>[] adj = new List<int>[V + 1];
        List<int>[] trans = new List<int>[V + 1];
        bool[] vis = new bool[V + 1];
        List<int> order = new List<int>();
        List<int> scc = new List<int>();

        int n;

        public Kosaraju(int nodes)
        {
            n = nodes;

            for (int i = 0; i <= V; i++)
            {
                adj[i] = new List<int>();
                trans[i] = new List<int>();
            }
        }

        void AddEdge(int a, int b)
        {
            adj[a].Add(b);
            trans[b].Add(a);
        }

        void FindSCC()
        {
            for (int i = 1; i <= n; i++)
            {
                if (!vis[i])
                    Dfs(i);
            }

            for (int i = 1; i <= n; i++)
                vis[i] = false;

            for (int i = 1; i <= n; i++)
            {
                if (!vis[order[n - i]])
                {
                    scc.Clear();
                    Dfs1(order[n - i]);

                    Console.WriteLine($"dfs1() was called from {order[n - i]} and printing SCC");
                    
                    foreach (var node in scc)
                        Console.Write(node + " ");
                    
                    Console.WriteLine();
                }
            }
        }

        void Dfs(int node)
        {
            vis[node] = true;

            foreach (var child in adj[node])
            {
                if (!vis[child])
                    Dfs(child);
            }

            order.Add(node);
        }

        void Dfs1(int node)
        {
            vis[node] = true;

            foreach (var child in trans[node])
            {
                if (!vis[child])
                    Dfs1(child);
            }

            scc.Add(node);
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
