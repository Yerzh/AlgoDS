using AlgoDS.DataStructures;
using System;
using System.Linq;

namespace AlgoDS
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, q, l, r, k;
            int[] input;

            input = ReadIntVals();
            n = input[0];
            q = input[1];

            int[] arr = new int[n + 1];

            for (int i = 1; i <= n; i++)
                arr[i] = int.Parse(Console.ReadLine());

            var mergeSortTree = new MergeSortTree(arr);
            mergeSortTree.Build(1, 1, n);

            while (q-- > 0)
            {
                input = ReadIntVals();
                l = input[0];
                r = input[1];
                k = input[2];
                Console.WriteLine(mergeSortTree.Query(1, 1, n, l, r, k));
            }
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