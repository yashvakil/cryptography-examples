using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    public class Element
    {
        public int a { get; set; }
        public int m { get; set; }
        public Element() { }
        public Element(int a, int m)
        {
            this.a = a;
            this.m = m;
        }
    }

    class RabinCryptoSystem
    {
        public static void Main(String[] args)
        {
            Random random = new Random();
            int p = 4, q = 4;

            while (MillerRabin(p, 2) && (p >= 1) && ((p + 1) % 4 == 0))
                p = random.Next(256);
            while (MillerRabin(q, 2) && (q >= 1) && ((q + 1) % 4 == 0))
                q = random.Next(256);

            p = 19; q = 11;
            int n = p * q;
            int m = random.Next(2, n - 1);
            m = 9;

            Console.WriteLine("\n\n");
            Console.WriteLine("Public Key: {0}", n);
            Console.WriteLine("Private Key: {0} {1}", p, q);

            int encoded = (m * m) % n;

            int a1 = (int)(Math.Pow(encoded, (p + 1) / 4) % p);
            int a2 = ((-1 * a1) + p) % p;
            int b1 = (int)(Math.Pow(encoded, (q + 1) / 4) % q);
            int b2 = ((-1 * b1) + q) % q;

            int p1 = ChineseRemainder(a1, b1, p, q);
            int p2 = ChineseRemainder(a1, b2, p, q);
            int p3 = ChineseRemainder(a2, b1, p, q);
            int p4 = ChineseRemainder(a2, b2, p, q);

            Console.WriteLine("\n\n");
            Console.WriteLine("Plain Text: {0}\nEncoded Text: {1}\nDecoded Text: {2} {3} {4} {5}", m, encoded, p1, p2, p3, p4);

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

        public static int ChineseRemainder(int n1, int n2, int p, int q)
        {
            List<Element> elements = new List<Element>();
            int M = p * q;
            elements.Add(new Element(n1, p));
            elements.Add(new Element(n2, q));

            int X = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                int gcd, Minverse;
                int MI = M / elements[i].m;

                Euclidean(MI, elements[i].m, out gcd, out Minverse);

                X += (elements[i].a * MI * Minverse);
            }

            X = X % M;

            return X;
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
