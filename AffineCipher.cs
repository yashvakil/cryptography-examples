using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class AffineCipher
    {
        public static void Main(String[] args)
        {
            bool errorFlag = false;
            int key1, key2;
            Console.Write("Enter Key 1:  ");
            if (!Int32.TryParse(Console.ReadLine(), out key1))
                errorFlag = true;
            Console.Write("Enter Key 2:  ");
            if (!Int32.TryParse(Console.ReadLine(), out key2))
                errorFlag = true;

            int gcd, inverse;
            if (!Euclidean(key1, 26, out gcd, out inverse))
                errorFlag = true;

            if (errorFlag)
            {
                Console.WriteLine("Enter Proper Values");
                System.Environment.Exit(1);
            }

            Console.Write("Enter Plain Text:  ");
            string ptext = Console.ReadLine();

            string encoded = Encode(ptext, key1, key2);
            string decoded = Decode(encoded, key1, key2);

            Console.WriteLine("\n\n");
            Console.WriteLine("Plain Text: {0}\nEncoded Text: {1}\nDecoded Text: {2}", ptext, encoded, decoded);

            Console.ReadKey();
        }

        public static string Encode(String pt, int k1, int k2)
        {
            string cipher = String.Empty;
            k1 = k1 % 26;
            k2 = k2 % 26;

            foreach(char ch in pt)
            {
                char c = ch;
                if (Char.IsLetter(c))
                {
                    char d = Char.IsUpper(ch) ? 'A' : 'a';
                    c = (char)(((((c - d) * k1) + k2) % 26) + d);
                }
                cipher += c;
            }

            return cipher;
        }

        public static string Decode(String cipher, int k1, int k2)
        {
            string pt = String.Empty;
            k1 = k1 % 26;
            k2 = k2 % 26;
            int gcd, inverse;
            Euclidean(k1, 26, out gcd, out inverse);

            k1 = inverse;
            k2 = 26 - k2;

            foreach (char ch in cipher)
            {
                char c = ch;
                if (Char.IsLetter(c))
                {
                    char d = Char.IsUpper(ch) ? 'A' : 'a';
                    c = (char)(((((c - d) + k2) * k1) % 26) + d);
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
