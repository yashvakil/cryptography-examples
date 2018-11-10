using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class ExtendedEuclidean
    {
        public static void Main(String[] args)
        {
            int a, n;
            bool errorFlag = false;

            Console.Write("Enter a: ");
            if (!Int32.TryParse(Console.ReadLine(), out a))
                errorFlag = true;
            Console.Write("Enter n: ");
            if (!Int32.TryParse(Console.ReadLine(), out n))
                errorFlag = true;

            if (errorFlag)
            {
                Console.WriteLine("Enter Proper Numbers");
                System.Environment.Exit(1);
            }

            int gcd, inverse;
            if(Euclidean(a,n,out gcd, out inverse))
                Console.WriteLine("GCD: {0}\nInverse: {1}", 1, inverse);
            else
                Console.WriteLine("GCD: {0}\nInverse: {1}", gcd, "Does Not Exist");

            Console.ReadKey();
        }

        public static bool Euclidean(int a, int n, out int gcd, out int inverse)
        {
            a = a % n;
            int r1 = n, r2 = a, t1 = 0, t2 = 1, q, r, t;
            while(r2 > 0)
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
