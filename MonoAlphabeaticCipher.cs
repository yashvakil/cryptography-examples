using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class MonoAlphabeaticCipher
    {
        public static void Main(String[] args)
        {
            Console.Write("Enter Plain Text:  ");
            string ptext = Console.ReadLine();

            Dictionary<char, char> key = getMapping();

            string encoded = Encode(ptext, key);
            string decoded = Decode(encoded, key);

            Console.WriteLine("\n\n");
            Console.WriteLine("Plain Text: {0}\nEncoded Text: {1}\nDecoded Text: {2}", ptext, encoded, decoded);

            Console.ReadKey();
        }

        public static Dictionary<char,char> getMapping()
        {
            Dictionary<char, char> map = new Dictionary<char, char>();
            //string str1 = "abcdefghijklmnopqrstuvwxyz";
            //string str2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string str1 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+-*/%[]{}()<>.,;?!\\\"'`";
            string str2 = "ABefghiDEFabcjklmHI;LJK)<,MN}(Oopq>.wxyz/%[12345]GQRSTrstuXYZ0Pn678vUVW9+Cd-*{?!\\\"'`";

            for (int i = 0; i < str1.Length; i++)
                map.Add(str1[i], str2[i]);

            return map;
        }

        public static string Encode(string pt, Dictionary<char,char> map)
        {
            string cipher = String.Empty;
            foreach (char ch in pt)
                cipher += map[ch];

            return cipher;
        }

        public static string Decode(string cipher, Dictionary<char,char> map)
        {
            string pt = String.Empty;
            foreach (char ch in cipher)
                pt += map.First(x => x.Value == ch).Key;

            return pt;
        }
    }
}
