namespace AlgoDS.Algorithms
{
    public class RabinKarp
    {
        const long P_B = 29;
        const long P_M = 1000000009;
        
        private long GetHash(string s) {
           long r = 0;
           for (int i = 0; i < s.Length; i++) {
              r = r * P_B + s[i];
              r %= P_M;
           }
           return r;
        }

        public int Search(string key, string text)
        {
            if (string.IsNullOrEmpty(key))
                return 0;

            long h1 = GetHash(key);
            long h2 = 0;
            long power = 1;
            for (int i = 0; i < key.Length; i++)
                power = (power * P_B) % P_M;

            for (int i = 0; i < text.Length; i++)
            {
                h2 = h2 * P_B + text[i];
                h2 %= P_M;
                if (i >= key.Length)
                {
                    h2 -= power * text[i - key.Length] % P_M;
                    if (h2 < 0)
                        h2 += P_M;
                }
                if (i >= key.Length - 1 && h1 == h2)
                    return i - (key.Length - 1);
            }
            return -1;
        }
    }
}
