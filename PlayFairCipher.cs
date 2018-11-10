using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    public class Position
    {

        public int row { get; set; }
        public int col { get; set; }

        public Position()
        {

        }
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    public class PositionEqualityComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position x, Position y)
        {
            return ((x.row == y.row) & (x.col == y.col));
        }

        public int GetHashCode(Position obj)
        {
            string combined = obj.row + "|" + obj.col;
            return (combined.GetHashCode());
        }
    }


    class PlayFairCipher
    {
        public static void Main(String[] args)
        {
            string key = String.Empty;

            Console.Write("Enter key:  ");
            key = Console.ReadLine();

            Console.Write("Enter Plain Text:  ");
            string ptext = Console.ReadLine();

            string encoded = Encode(ptext, key);
            string decoded = Decode(encoded, key);

            Console.WriteLine("\n\n");
            Console.WriteLine("Plain Text: {0}\nEncoded Text: {1}\nDecoded Text: {2}", ptext, encoded, decoded);
            Console.ReadKey();

        }

        public static Dictionary<Position, char> createMatrix(string key)
        {
            string constant = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+-*/%[]{}()<>.,;?!\\\"'`" ;
            Dictionary<Position, char> matrix = new Dictionary<Position, char>(new PositionEqualityComparer());

            key = new String(key.Distinct().ToArray());
            foreach (char c in key)
                constant = constant.Replace(c.ToString(), String.Empty);
            
            constant = key + constant;
            int i = 0, j = 0;
            foreach(char c in constant)
            {
                matrix.Add(new Position(i, j), c);

                i = (j == 5) ? i + 1 : i ;
                j = (j < 5) ? j + 1 : 0 ;
            }

            return matrix;
        }

        public static string Encode(String pt, string key)
        {
            string cipher = String.Empty;
            Dictionary<Position, char> matrix = createMatrix(key);

            for(int i = 0; i < pt.Length; i = i + 2)
            {
                if (i + 1 < pt.Length && pt[i] == pt[i + 1])
                    pt = pt.Insert(i + 1, "x");
            }

            if (pt.Length % 2 != 0)
                pt += "x";

            for(int i = 0; i < pt.Length; i = i + 2)
            {
                char ch;
                Position p1 = matrix.First(x => x.Value == pt[i]).Key;
                Position p2 = matrix.First(x => x.Value == pt[i+1]).Key;
                Position p3 = new Position(), p4 = new Position();
                if(p1.row == p2.row)
                {
                    p3.row = p4.row = p1.row;
                    p3.col = (p1.col + 1) % 6;
                    p4.col = (p2.col + 1) % 6;
                }
                else if (p1.col == p2.col)
                {
                    p3.col = p4.col = p1.col;
                    p3.row = (p1.row + 1) % 6;
                    p4.row = (p2.row + 1) % 6;
                }
                else
                {
                    p3.row = p1.row;
                    p3.col = p2.col;

                    p4.row = p2.row;
                    p4.col = p1.col;
                }
                cipher = cipher + matrix[p3] + matrix[p4];

            }
            
            return cipher;
        }

        public static string Decode(string cipher, string key)
        {
            string pt = String.Empty;
            Dictionary<Position, char> matrix = createMatrix(key);

            for (int i = 0; i < cipher.Length; i = i + 2)
            {
                char ch;
                Position p1 = matrix.First(x => x.Value == cipher[i]).Key;
                Position p2 = matrix.First(x => x.Value == cipher[i+1]).Key;

                Position p3 = new Position(), p4 = new Position();
                if (p1.row == p2.row)
                {
                    p3.row = p4.row = p1.row;
                    p3.col = (p1.col - 1) % 6;
                    p4.col = (p2.col - 1) % 6;
                }
                else if (p1.col == p2.col)
                {
                    p3.col = p4.col = p1.col;
                    p3.row = (p1.row - 1) % 6;
                    p4.row = (p2.row - 1) % 6;
                }
                else
                {
                    p3.row = p1.row;
                    p3.col = p2.col;

                    p4.row = p2.row;
                    p4.col = p1.col;
                }
                pt = pt + matrix[p3] + matrix[p4];

            }

            return pt;
        }

    }
}
