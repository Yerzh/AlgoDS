namespace AlgoDS.DataStructures
{
    public class UnionFind
    {
        int[] parent;

        public UnionFind(int n)
        {
            parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
            }
        }

        public void Display()
        {
            for (int i = 0; i < parent.Length; i++)
            {
                System.Console.Write(parent[i] + " ");
            }
            System.Console.WriteLine();
        }

        public int Find(int x)
        {
            if (parent[x] == x)
                return x;

            return parent[x] = Find(parent[x]);
        }

        public void Union(int x, int y)
        {
            x = Find(x);
            y = Find(y);
            parent[x] = y;
        }
    }
}
