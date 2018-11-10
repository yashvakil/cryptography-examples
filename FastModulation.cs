using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class FastModulation
    {
        public static void Main(String[] args)
        {
            Console.WriteLine(FastModulate(8, 320, 200));
            Console.ReadKey();
        }

        public static int FastModulate(int a, int b, int n)
        {
            string bi = Convert.ToString(b, 2);
            int c = 0, f = 1;

            for(int i = 0; i < bi.Length; i++)
            {
                c = 2 * c;
                f = (f * f) % n;
                if((bi[i] - '0') == 1)
                {
                    c = c + 1;
                    f = (f * a) % n;
                }

            }

            return f;
        }
    }
    
}
