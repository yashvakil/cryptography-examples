using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class MillerRabinTest
    {
        public static void Main(String[] args)
        {
            Random random = new Random();
            int num = -1;
            while (num <= 1)
                num = random.Next();
            
            if (MillerRabin(num, 2))
                Console.WriteLine("{0} is PRIME", num);
            else
                Console.WriteLine("{0} is COMPOSITE", num);

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

            while(i < k - 1)
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

    }
}
