using System;
using System.Collections.Generic;

namespace AlgoDS.Algorithms.Graphs
{
    public class DfsTree
    {
        const int V = 100001;
        List<int>[] adj = new List<int>[V + 1];
        bool[] vis = new bool[V + 1];
        int[] inTime = new int[V + 1];
        int[] low = new int[V + 1];
        int time = 0;

        //Find Cut edge
        public void FindBridge(int node, int parent = -1)
        {
            vis[node] = true;
            inTime[node] = low[node] = time;
            time++;

            foreach(var child in adj[node])
            {
                if (child == parent)
                    continue;

                if (vis[child])
                {
                    //edge (node <--> child) is a back edge

                    low[node] = Math.Min(low[node], inTime[child]);
                }
                else
                {
                    //edge (node <--> child) is a forward edge

                    FindBridge(child, node);

                    if (low[child] > inTime[node])
                        Console.WriteLine($"{node} - {child} is a bridge");

                    low[node] = Math.Min(low[node], low[child]);
                }
            }
        }

        //Find Cut vertex
        public void FindArticulationPoint(int node, int parent = -1)
        {
            vis[node] = true;
            inTime[node] = low[node] = time++;

            int children = 0;
            foreach(var child in adj[node])
            {
                if (child == parent)
                    continue;

                if (vis[child])
                {
                    low[node] = Math.Min(low[node], inTime[child]);
                }
                else
                {
                    FindArticulationPoint(child, node);
                    low[node] = Math.Min(low[node], low[child]);
                    if (low[child] >= inTime[node] && parent != -1)
                        Console.WriteLine($"{node} is an articulation point");
                    children++;
                }
            }

            if (parent == -1 && children > 1)
                Console.WriteLine($"{node} is an articulation point");
        }
    }
}

//The relation between Articulation point(Cut vertex) and Bridge
//1. Endpoints of a bridge will be an articulation point if that node has a degree at least 2.
//2. It is not necessary for an articulation point to be an endpoint of a bridge.
//3. Finding bridges algorithm cannot be used to find articulation point.