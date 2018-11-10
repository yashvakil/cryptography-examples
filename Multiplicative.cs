using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class Multiplicative
    {
        public static void Main(String[] args)
        {
            bool errorFlag = false;
            int key;
            Console.Write("Enter Key:  ");
            if (!Int32.TryParse(Console.ReadLine(), out key))
                errorFlag = true;

            int gcd, inverse;
            if (!Euclidean(key, 26, out gcd, out inverse))
                errorFlag = true;

            if (errorFlag)
            {
                Console.WriteLine("Enter Proper Values");
                System.Environment.Exit(1);
            }

            Console.Write("Enter Plain Text:  ");
            string ptext = Console.ReadLine();

            string encoded = Encode(ptext, key);
            string decoded = Decode(encoded, key);

            Console.WriteLine("\n\n");
            Console.WriteLine("Plain Text: {0}\nEncoded Text: {1}\nDecoded Text: {2}", ptext, encoded, decoded);

            Console.ReadKey();
        }

        public static string Encode(String pt, int k)
        {
            string cipher = String.Empty;
            k = k % 26;

            foreach (char ch in pt)
            {
                char c = ch;
                if (Char.IsLetter(c))
                {
                    char d = Char.IsUpper(ch) ? 'A' : 'a';
                    c = (char)((((c - d) * k) % 26) + d);
                }
                cipher += c;
            }

            return cipher;
        }

        public static string Decode(String cipher, int k)
        {
            string pt = String.Empty;
            k = k % 26;
            int gcd, inverse;
            Euclidean(k, 26, out gcd, out inverse);

            k = inverse;

            foreach (char ch in cipher)
            {
                char c = ch;
                if (Char.IsLetter(c))
                {
                    char d = Char.IsUpper(ch) ? 'A' : 'a';
                    c = (char)((((c - d) * k) % 26) + d);
                }
                pt += c;
            }

            return pt;
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
