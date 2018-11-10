using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class PrimitiveRoots
    {
        public static void Main(String[] args)
        {
            int n;
            Console.Write("Enter n:  ");
            if (!(Int32.TryParse(Console.ReadLine(), out n)))
            {
                Console.WriteLine("Enter Proper Number");
                System.Environment.Exit(1);
            }

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

            foreach (int i in primitiveRoots)
                Console.WriteLine(i);
            Console.ReadKey();
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
