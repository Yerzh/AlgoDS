using System;

namespace AlgoDS.DataStructures
{
    public class SegmentTree
    {
        int[] data;
        int[] tree;
        int[] lazy;

        public SegmentTree(int[] data)
        {
            this.data = data; //1-based array is more convenient here
            this.tree = new int[4 * data.Length]; //It always takes 4 * n space
            this.lazy = new int[4 * data.Length]; //It always takes 4 * n space
        }

        //Range minimum query
        public void BuildRMQ(int si, int ss, int se)
        {
            if (ss == se)
            {
                tree[si] = data[ss];
                return;
            }

            int mid = (ss + se) / 2;

            BuildRMQ(2 * si, ss, mid);
            BuildRMQ(2 * si + 1, mid + 1, se);

            tree[si] = Math.Min(tree[2 * si], tree[2 * si + 1]);
        }

        //Range sum query
        public void BuildRSQ(int si, int ss, int se)
        {
            if (ss == se)
            {
                tree[si] = data[ss];
                return;
            }

            int mid = (ss + se) / 2;

            BuildRSQ(2 * si, ss, mid);
            BuildRSQ(2 * si + 1, mid + 1, se);

            tree[si] = tree[2 * si] + tree[2 * si + 1];
        }

        public int RMQ(int si, int ss, int se, int qs, int qe)
        {
            if (qs > se || qe < ss)    //completely outside
                return int.MaxValue;

            if (qs <= ss && se <= qe)  //completely inside
                return tree[si];

            int mid = (ss + se) / 2;

            int l = RMQ(si * 2, ss, mid, qs, qe);
            int r = RMQ(si * 2 + 1, mid + 1, se, qs, qe);

            return Math.Min(l, r);
        }

        public int RSQ(int si, int ss, int se, int qs, int qe)
        {
            if (lazy[si] != 0) //lazy propagation
            {
                int dx = lazy[si];
                lazy[si] = 0;
                tree[si] += dx * (se - ss + 1);

                if (ss != se)
                {
                    lazy[2 * si] += dx;
                    lazy[2 * si + 1] += dx;
                }
            }

            if (qs > se || qe < ss)    //completely outside
                return 0;

            if (qs <= ss && se <= qe)  //completely inside
                return tree[si];

            int mid = (ss + se) / 2;

            return RSQ(si * 2, ss, mid, qs, qe) + RSQ(si * 2 + 1, mid + 1, se, qs, qe);
        }

        public void UpdateElementRMQ(int idx, int value)
        {
            data[idx] = value;

            UpdateRMQ(1, 1, data.Length, idx);
        }

        void UpdateRMQ(int si, int ss, int se, int qi)
        {
            if (ss == se)
            {
                tree[si] = data[ss];
                return;
            }

            int mid = (ss + se) / 2;

            if (qi <= mid)
                UpdateRMQ(2 * si, ss, mid, qi);
            else
                UpdateRMQ(2 * si + 1, mid + 1, se, qi);

            tree[si] = Math.Min(tree[2 * si], tree[2 * si + 1]);
        }

        //Increment values of range from qs to qe by diff
        public void UpdateRangeRSQ(int si, int ss, int se, int us, int ue, int diff)
        {
            if (lazy[si] != 0)  //lazy propagation
            {
                int dx = lazy[si];
                lazy[si] = 0;
                tree[si] += dx * (se - ss + 1);

                if (ss != se)
                {
                    lazy[2 * si] += dx;
                    lazy[2 * si + 1] += dx;
                }
            }

            if (ss > se || ss > ue || se < us)
                return;

            if (ss >= us && se <= ue)
            {
                int dx = (se - ss + 1) * diff;
                tree[si] += dx;

                if (ss != se)
                {
                    lazy[2 * si] += diff;
                    lazy[2 * si + 1] += diff;
                }
                return;
            }

            int mid = (ss + se) / 2;
            UpdateRangeRSQ(2 * si, ss, mid, us, ue, diff);
            UpdateRangeRSQ(2 * si + 1, mid + 1, se, us, ue, diff);

            tree[si] = tree[2 * si] + tree[2 * si + 1];
        }
    }
}
