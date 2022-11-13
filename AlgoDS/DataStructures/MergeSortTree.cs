using System.Collections.Generic;

namespace AlgoDS.DataStructures
{
    internal class MergeSortTree
    {
        /*
         Problem:
         Given an array of size N and Q queries of form L R K, find number of elements in the range L to R 
         which are strictly smaller than K
         Array: 1 4 3 5 6 4 3 2 4 1
         1: Query: 1 5 4
            Answer: 2
         2: Query: 7 10 4
            Answer: 3
         */


        int[] data;
        List<int>[] tree;

        public MergeSortTree(int[] data)
        {
            this.data = data; //1-based array is more convenient here

            this.tree = new List<int>[4 * data.Length];
            for (int i = 1; i < tree.Length; i++)
                tree[i] = new List<int>();
        }

        //O(Nlog(N))
        public void Build(int si, int ss, int se)
        {
            if (ss == se)
            {
                tree[si].Add(data[ss]);
                return;
            }

            int mid = (ss + se) / 2;
            Build(2 * si, ss, mid);
            Build(2 * si + 1, mid + 1, se);

            int i = 0;
            int j = 0;

            while (i < tree[2 * si].Count && j < tree[2 * si + 1].Count)
            {
                if (tree[2 * si][i] <= tree[2 * si + 1][j])
                {
                    tree[si].Add(tree[2 * si][i]);
                    i++;
                }
                else
                {
                    tree[si].Add(tree[2 * si + 1][j]);
                    j++;
                }
            }

            while (i < tree[2 * si].Count)
            {
                tree[si].Add(tree[2 * si][i]);
                i++;
            }

            while (j < tree[2 * si + 1].Count)
            {
                tree[si].Add(tree[2 * si + 1][j]);
                j++;
            }
        }

        //O(log(N))
        public int Query(int si, int ss, int se, int qs, int qe, int k)
        {
            if (ss > qe || se < qs)
                return 0;

            if (ss >= qs && se <= qe)
                return UpperBound(tree[si], k - 1); 

            int mid = (ss + se) / 2;
            int l = Query(2 * si, ss, mid, qs, qe, k);
            int r = Query(2 * si + 1, mid + 1, se, qs, qe, k);

            return l + r;
        }

        public static int UpperBound(List<int> list, int X)
        {
            int low = 0;
            int high = list.Count;

            while (low < high)
            {
                int mid = low + (high - low) / 2;
                if (X >= list[mid])
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid;
                }
            }

            // if X is greater than arr[n-1]
            if (low < list.Count && list[low] <= X)
            {
                low++;
            }

            return low;
        }
    }
}
