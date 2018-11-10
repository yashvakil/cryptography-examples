using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class CeaserCipher
    {
        static void Main(string[] args)
        {
            int key = 0;
            Console.Write("Enter key: ");
            if (!Int32.TryParse(Console.ReadLine(), out key))
            {
                Console.WriteLine("Please enter a proper digit");
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

        public static string Encode(string pt, int key)
        {
            key = key % 26;
            string cipher = String.Empty;
            foreach(char ch in pt)
            {
                char c = ch;
                if (Char.IsLetter(c))
                {
                    char d = char.IsUpper(ch) ? 'A' : 'a';
                    c = (char)((((ch + key) - d) % 26) + d);
                }

                cipher += c;
            }

            return cipher;
        }

        public static string Decode(string cipher, int key)
        {
            key = key % 26;
            return Encode(cipher, 26 - key);
        }


    }
}
