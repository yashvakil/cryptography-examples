using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class Elgamal
    {
        public static Random random = new Random();
        public static void Main(String[] args)
        {
            
            int p = 4;
            while (!MillerRabin(p, 2))
                p = random.Next(3, 256);

            int d = random.Next(1, p - 2);
            List<int> roots = primitiveRoots(p);
            int e1 = roots[random.Next(roots.Count)];
            int e2 = FastModulate(e1, d, p);

            Console.Write("Enter Plain Text:  ");
            int ptext = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\n\n");
            Console.WriteLine("Public Key: {0} {1} {2}", e1, e2, p);
            Console.WriteLine("Private Key: {0} {1}", d, p);

            
            List<int> encoded = Encode(ptext, e1, e2, p);
            int decoded = Decode(encoded, d, p);

            Console.WriteLine("\n\n");
            Console.WriteLine("Plain Text: {0}\nEncoded Text: {1} {2}\nDecoded Text: {3}", ptext, encoded[0], encoded[1], decoded);
            Console.ReadKey();
        }

        public static List<int> Encode(int pt, int e1, int e2, int p)
        {
            List<int> cipher = new List<int>();
            int r = random.Next(p - 1);
            int c1 = FastModulate(e1, r, p);
            int c2 = pt * FastModulate(e2, r, p);

            cipher.Add(c1);
            cipher.Add(c2);

            return cipher;
        }

        public static int Decode(List<int> cipher, int d, int p)
        {
            int c1 = cipher[0];
            int c2 = cipher[1];

            c1 = FastModulate(c1, d, p);
            int gcd, inverse;
            Euclidean(c1, p, out gcd, out inverse);

            int pt = FastModulate(c2 * inverse, 1, p);

            return pt;
        }

        public static List<int> primitiveRoots(int n)
        {
            int pn = Phi(n);
            List<int> primitiveRoots = new List<int>();
            if (pn == 1)
            {
                primitiveRoots.Add(1);
            }
            int gcd, inverse;
            for (int i = 2; i < n && Euclidean(i, n, out gcd, out inverse); i++)
            {
                for (int j = 1; j < n; j++)
                {
                    int temp = FastModulate(i, j, n);
                    if (temp == 1)
                    {
                        if (j == pn)
                            primitiveRoots.Add(i);
                        break;
                    }
                }
            }

            return primitiveRoots;
        }

        public static int Phi(int n)
        {
            List<int> factors = primeFactors(n);
            List<int> distinct = factors.Distinct().ToList<int>();
            int pn = 1;

            foreach (int i in distinct)
            {
                int c;
                if ((c = factors.Where(x => x == i).Count()) == 1)
                    pn *= (i - 1);
                else
                {
                    int temp = (int)(Math.Pow(i, c) - Math.Pow(i, c - 1));
                    pn *= temp;
                }
            }

            return pn;
        }
        public static List<int> primeFactors(int n)
        {
            List<int> factors = new List<int>();
            while (n % 2 == 0)
            {
                factors.Add(2);
                n /= 2;
            }

            for (int i = 3; i <= Math.Sqrt(n); i += 2)
            {
                while (n % i == 0)
                {
                    factors.Add(i);
                    n /= i;
                }
            }

            if (n > 2)
                factors.Add(n);
            factors.Sort();

            return factors;

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
