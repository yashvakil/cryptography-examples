using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class Vigenere
    {
        public static void Main(String[] args)
        {
            bool flag = true;
            bool errorFlag = false;
            List<int> key = new List<int>();
            while (flag)
            {
                int num;
                Console.Write("Enter number: ");
                if (!Int32.TryParse(Console.ReadLine(), out num))
                {
                    Console.WriteLine("Enter Proper Number");
                    System.Environment.Exit(1);
                }
                key.Add(num);

                Console.Write("Enter More?:  ");
                string str = Console.ReadLine();
                if (!(str.StartsWith("Y") || str.StartsWith("y") || str.StartsWith("1")))
                    flag = false;
            }

            Console.Write("Enter Plain Text:  ");
            string ptext = Console.ReadLine();

            string encoded = Encode(ptext, key);
            string decoded = Decode(encoded, key);

            Console.WriteLine("\n\n");
            Console.WriteLine("Plain Text: {0}\nEncoded Text: {1}\nDecoded Text: {2}", ptext, encoded, decoded);
            Console.ReadKey();
        }

        public static string Encode(string pt, List<int> key)
        {
            string cipher = String.Empty;
            int i = 0;
            foreach(char ch in pt)
            {
                char c = ch;
                if (Char.IsLetter(c))
                {
                    char d = Char.IsUpper(c) ? 'A' : 'a';
                    c = (char)((((c - d) + key[i]) % 26) + d);
                    i = (i < key.Count - 1) ? i + 1 : 0; 
                }
                cipher += c;
            }
            return cipher;
        }

        public static string Decode(string cipher, List<int> key)
        {
            string pt = String.Empty;
            int i = 0;
            foreach (char ch in cipher)
            {
                char c = ch;
                if (Char.IsLetter(c))
                {
                    char d = Char.IsUpper(c) ? 'A' : 'a';
                    c = (char)((((c - d) - key[i]) % 26) + d);
                    i = (i < key.Count - 1) ? i + 1 : 0;
                }
                pt += c;
            }
            return pt;
        }
    }
}
