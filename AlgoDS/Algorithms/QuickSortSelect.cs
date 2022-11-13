namespace AlgoDS.Algorithms
{
    public static class QuickSortSelect
    {
        static int Partition(int[] array, int left, int right)
        {
            int temp;
            int pos = left;
            int pivot = array[right];
            for (int i = left; i < right; i++)
            {
                if (pivot >= array[i])
                {
                    temp = array[pos];
                    array[pos] = array[i];
                    array[i] = temp;
                    pos++;
                }
            }
            temp = array[pos];
            array[pos] = array[right];
            array[right] = temp;
            return pos;
        }

        public static void QuickSort(int[] array, int left, int right)
        {
            if (left > right)
                return;

            int pivot = Partition(array, left, right);
            QuickSort(array, left, pivot - 1);
            QuickSort(array, pivot + 1, right);
        }

        public static int KthSmallest(int[] array, int left, int right, int k)
        {
            int partition = Partition(array, left, right);

            if (k == partition)
                return array[partition];

            if (k > partition)
                return KthSmallest(array, partition + 1, right, k);
            else
                return KthSmallest(array, left, partition - 1, k);
        }
    }
}
