using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class RSA
    {
        public static void Main(String[] args)
        {
            int p = 4, q = 4;
            Random random = new Random();
            while (MillerRabin(p, 2) && p >= 1)
                p = random.Next(256);
            while (MillerRabin(q, 2) && p >= 1)
                q = random.Next(256);

            int n = p * q;
            int pn = (p - 1) * (q - 1);
            int e = random.Next(pn - 1);
            int d, m = random.Next(2, pn - 1);
            int gcd;

            while (!Euclidean(e, pn, out gcd, out d))
                e = random.Next(2, pn - 1);

            Console.WriteLine("\n\n");
            Console.WriteLine("Public Key: {0} {1}", e, n);
            Console.WriteLine("Private Key: {0} {1}", d, n);

            int encoded = FastModulate(m, e, n);
            int decoded = FastModulate(encoded, d, n);

            Console.WriteLine("\n\n");
            Console.WriteLine("Plain Text: {0}\nEncoded Text: {1}\nDecoded Text: {2}", m, encoded, decoded);

            Console.ReadKey();
        }

        public static bool MillerRabin(int num, int a)
        {
            int m = 0, k = 0, t = 0, i = 0;
            if (num == 2)
                return true;
            else if (num % 2 == 0)
                return false;

            int n = num - 1;
            while (n % 2 == 0)
            {
                n = n / 2;
                k++;
            }
            m = n;

            t = FastModulate(a, m, num);
            if (t == 1 || t == num - 1)
                return true;

            while (i < k - 1)
            {
                t = FastModulate(t, 2, num);
                if (t == 1)
                    return false;
                if (t == num - 1)
                    return true;
            }

            return false;

        }

        public static int FastModulate(int a, int b, int n)
        {
            string bi = Convert.ToString(b, 2);
            int c = 0, f = 1;

            for (int i = 0; i < bi.Length; i++)
            {
                c = 2 * c;
                f = (f * f) % n;
                if ((bi[i] - '0') == 1)
                {
                    c = c + 1;
                    f = (f * a) % n;
                }

            }

            return f;
        }

        public static bool Euclidean(int a, int n, out int gcd, out int inverse)
        {
            a = a % n;
            int r1 = n, r2 = a, t1 = 0, t2 = 1, q, r, t;
            while (r2 > 0)
            {
                q = r1 / r2;
                r = r1 - q * r2;
                r1 = r2;
                r2 = r;
                t = t1 - q * t2;
                t1 = t2;
                t2 = t;
            }
            gcd = r1;
            inverse = t1;
            while (inverse < 0)
                inverse += n;

            if (gcd == 1)
                return true;
            else
                return false;
        }
    }
}
