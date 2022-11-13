using AlgoDS.DataStructures;
using System.Collections.Generic;

namespace AlgoDS.Algorithms
{
    public class Kruskal
    {
        //O(ElogE + ElogV)
        public int MinimumSpanningTreeWeight(List<int[]> edges, int v)
        {
            edges.Sort((a, b) => a[2].CompareTo(b[2]));
            var uf = new UnionFind(v);
            int sum = 0;
            foreach(var e in edges)
            {
                if (uf.Find(e[0] - 1) != uf.Find(e[1] - 1))
                {
                    uf.Union(e[0] - 1, e[1] - 1);
                    sum += e[2];
                }                
            }
            return sum;
        }
    }
}
