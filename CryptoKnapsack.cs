using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cryptography
{
    class CryptoKnapsack
    {
        public static void Main(String[] args)
        {
            List<int> a = new List<int>();
            List<int> b = new List<int>();
            int q, r, gcd, rin, encoded = 0, total = 0;
            string decoded = String.Empty, ptext = String.Empty;

            bool flag = true;
            bool errorFlag = false;

            while (flag)
            {
                int temp;
                Console.Write("Enter a Number:  ");
                if (!(Int32.TryParse(Console.ReadLine(), out temp)))
                    errorFlag = true;
                if (!(temp > total))
                    errorFlag = true;

                if (errorFlag)
                {
                    Console.WriteLine("Enter Proper Numbers");
                    System.Environment.Exit(1);
                }

                a.Add(temp);
                total += temp;

                Console.Write("Enter More?:  ");
                string str = Console.ReadLine();
                if (!(str.StartsWith("Y") || str.StartsWith("y") || str.StartsWith("1")))
                    flag = false;
                
            }

            Console.Write("Enter q Greater than {0}:  ",total);
            if (!(Int32.TryParse(Console.ReadLine(), out q)))
                errorFlag = true;
            Console.Write("Enter r:  ");
            if (!(Int32.TryParse(Console.ReadLine(), out r)))
                errorFlag = true;

            if (!(Euclidean(r, q, out gcd, out rin)))
                errorFlag = true;
            
            Console.Write("Enter Plain Text: ");
            ptext = Console.ReadLine();

            if (!Regex.IsMatch(ptext, "^[0-1]{" + a.Count + "}$"))
                errorFlag = true;

            if (errorFlag)
            {
                Console.WriteLine("Enter Proper Numbers");
                System.Environment.Exit(1);
            }

            char[] array = ptext.ToCharArray();
            for (int i = 0; i < a.Count; i++)
            {
                b.Add((a[i] * r) % q);
                encoded += (b[i] * (array[i] - '0'));
            }

            Console.WriteLine("\n\n");
            Console.WriteLine("Plain Text: {0}\nEncoded Text: {1}", ptext, encoded);
            encoded = encoded * rin;
            encoded = encoded % q;
            for (int i = a.Count - 1; i >= 0; i--)
            {
                if (encoded >= a[i])
                {
                    decoded = '1' + decoded;
                    encoded = encoded - a[i];
                }
                else
                    decoded = '0' + decoded;
            }
            Console.WriteLine("Decoded Text: {0}", decoded);

            Console.ReadKey();

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
