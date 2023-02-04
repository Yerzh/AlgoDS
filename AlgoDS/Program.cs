using AlgoDS.Algorithms.Graphs;
using AlgoDS.DataStructures;
using System;
using System.Linq;

namespace AlgoDS
{
    class Program
    {
        static void Main(string[] args)
        {
            //int n = 3;
            //int[][] queries = new int[3][]
            //{
            //    new int[] { 5, 3 },
            //    new int[] { 4, 7 },
            //    new int[] { 2, 3 },
            //};

            int n = 28;
            int[][] queries = new int[1][]
            {
                new int[] { 1, 2 }
            };

            var res = CycleLengthQueries(n, queries);

            foreach(var i in res)
            {
                Console.Write(i + " ");
            }
        }

        static int[] CycleLengthQueries(int n, int[][] queries)
        {
            int v = (int)Math.Pow(2, n) - 1;
            var bl = new BinaryLifting(v);

            for (int i = 1; i <= n; i++)
            {
                bl.AddEdge(i, 2 * i);
                bl.AddEdge(i, 2 * i + 1);
            }

            bl.Preprocess();

            int m = queries.Length;
            int[] ans = new int[m];
            for (int i = 0; i < m; i++)
            {
                int a = queries[i][0];
                int b = queries[i][1];

                int dist = bl.GetDistance(a, b);
                ans[i] = dist + 1;
            }

            return ans;
        }

        static int[] ReadIntVals() =>
            Console.ReadLine().Split(" ").Select(num => int.Parse(num)).ToArray();
    }
}

/*
10 55555
1 1 10
2 1 1 10
1 1 10
2 1 10 10

 */