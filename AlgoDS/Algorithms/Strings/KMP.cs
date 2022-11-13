using System.Collections.Generic;

namespace AlgoDS.Algorithms
{
    public class KMP
    {
        int[] lps;

        public IList<int> Search(string pat, string txt)
        {
            var indices = new List<int>();
            if (string.IsNullOrEmpty(pat))
                return indices;

            int M = pat.Length;
            int N = txt.Length;

            computeLPSArray(pat);

            int i = 0;
            int j = 0;
            while (i < N)
            {
                if (pat[j] == txt[i])
                {
                    j++;
                    i++;
                }
                
                if (j == M)
                {
                    indices.Add(i - j);
                    j = lps[j - 1];
                }
                else if (i < N && pat[j] != txt[i])
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i++;
                }
            }

            return indices;
        }

        void computeLPSArray(string pat)
        {
            int M = pat.Length;
            lps = new int[M];
            int len = 0;
            int i = 1;
            lps[0] = 0;
            while (i < M)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }
        }
    }
}
