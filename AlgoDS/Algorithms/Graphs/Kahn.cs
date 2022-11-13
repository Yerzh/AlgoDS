using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoDS.Algorithms.Graphs
{
    public static class Kahn
    {
        static List<int>[] adj = new List<int>[100];
        static List<int> result = new List<int>();
        static int[] indegree = new int[100];

        static Kahn()
        {
            for (int i = 0; i < adj.Length; i++)
                adj[i] = new List<int>();
        }

        public static void Run()
        {
            var input = ReadIntegers();
            int V = input[0];
            int E = input[1];

            int x, y;
            for (int i = 1; i <= E; i++)
            {
                input = ReadIntegers();
                x = input[0];
                y = input[1];

                if (adj[x] == null)
                    adj[x] = new List<int>();
                adj[x].Add(y);
                indegree[y]++;
            }

            TopologicalSort(V);
        }

        static void TopologicalSort(int v)
        {
            var q = new Queue<int>();
            for (int i = 1; i <= v; i++)
            {
                if (indegree[i] == 0)
                    q.Enqueue(i);
            }

            while(q.Count > 0)
            {
                int curr = q.Dequeue();
                result.Add(curr);

                foreach (var node in adj[curr])
                {
                    indegree[node]--;
                    if (indegree[node] == 0)
                        q.Enqueue(node);
                }
            }

            Console.Write("TopSort : ");
            foreach(var node in result)
            {
                Console.Write(node + " ");
            }

            //To check if DAG has a cycle
            //return result.Count == v;
        }

        static void TopologicalSortPQ(int v)
        {
            var pq = new PriorityQueue<int, int>();
            for (int i = 1; i <= v; i++)
            {
                if (indegree[i] == 0)
                    pq.Enqueue(i, i);
            }

            while (pq.Count > 0)
            {
                int curr = pq.Dequeue();
                result.Add(curr);

                foreach (var node in adj[curr])
                {
                    indegree[node]--;
                    if (indegree[node] == 0)
                        pq.Enqueue(node, node);
                }
            }

            Console.Write("TopSort : ");
            foreach (var node in result)
            {
                Console.Write(node + " ");
            }

            //To check if DAG has a cycle
            //return result.Count == v;
        }

        static int[] ReadIntegers()
        {
            var input = Console.ReadLine();
            return input.Split(' ').Select(i => int.Parse(i)).ToArray();
        }
    }
}


//Custom comparer
//class TitleComparer : IComparer<string>
//{
//    public int Compare(string titleA, string titleB)
//    {
//        var titleAIsFancy = titleA.Equals("sir", StringComparison.InvariantCultureIgnoreCase);
//        var titleBIsFancy = titleB.Equals("sir", StringComparison.InvariantCultureIgnoreCase);


//        if (titleAIsFancy == titleBIsFancy) //If both are fancy (Or both are not fancy, return 0 as they are equal)
//        {
//            return 0;
//        }
//        else if (titleAIsFancy) //Otherwise if A is fancy (And therefore B is not), then return -1
//        {
//            return -1;
//        }
//        else //Otherwise it must be that B is fancy (And A is not), so return 1
//        {
//            return 1;
//        }
//    }
//}