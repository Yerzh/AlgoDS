namespace AlgoDS.Algorithms
{
    public static class BinarySearch
    {
        public static int LowerBound(int[] arr, int X)
        {
            int low = 0;
            int high = arr.Length;

            while (low < high)
            {
                int mid = low + (high - low) / 2;
                if (X <= arr[mid])
                {
                    high = mid;
                }
                else
                {
                    low = mid + 1;
                }
            }

            // if X is greater than arr[n-1]
            if (low < arr.Length && arr[low] < X)
            {
                low++;
            }
            return low;
        }

        public static int UpperBound(int[] arr, int X)
        {
            int low = 0;
            int high = arr.Length;

            while (low < high)
            {
                int mid = low + (high - low) / 2;
                if (X >= arr[mid])
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid;
                }
            }

            // if X is greater than arr[n-1]
            if (low < arr.Length && arr[low] <= X)
            {
                low++;
            }

            return low;
        }
    }
}
