using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    public class Element
    {
        public int a { get; set; }
        public int m { get; set; }
        public Element() { }
        public Element(int a, int m)
        {
            this.a = a;
            this.m = m;
        }
    }

    class ChineseRemainder
    {
        public static void Main(String[] args)
        {
            bool flag = true;
            bool errorFlag = false;
            List<Element> elements = new List<Element>();

            while (flag)
            {
                int a, m;
                Console.Write("Enter a:  ");
                if (!Int32.TryParse(Console.ReadLine(), out a))
                    errorFlag = true;

                Console.Write("Enter m:  ");
                if (!Int32.TryParse(Console.ReadLine(), out m))
                    errorFlag = true;

                if (errorFlag)
                {
                    Console.WriteLine("Enter Proper Numbers");
                    System.Environment.Exit(1);
                }

                elements.Add(new Element(a, m));
                Console.Write("Enter More?:  ");
                string str = Console.ReadLine();
                if (!(str.StartsWith("Y") || str.StartsWith("y") || str.StartsWith("1")))
                    flag = false;
            }

            int M = 1;

            for (int i = 0; i < elements.Count; i++)
            {
                M *= elements[i].m;
                for (int j = i + 1; j < elements.Count; j++)
                {
                    Element e1 = elements[i];
                    Element e2 = elements[j];
                    int gcd, inverse;
                    if(!(Euclidean(e1.m, e2.m,out gcd, out inverse)))
                    {
                        Console.WriteLine("Enter Proper Numbers");
                        System.Environment.Exit(1);
                    }
                }
            }

            int X = 0;
            for(int i = 0; i < elements.Count; i++)
            {
                int gcd,Minverse;
                int MI = M / elements[i].m;
                
                Euclidean(MI, elements[i].m, out gcd, out Minverse);
                
                X += (elements[i].a * MI * Minverse);
            }

            X = X % M;
            Console.WriteLine("X:  {0}", X);

            Console.ReadKey();
            
        }

        public static bool Euclidean(int a, int n, out int gcd, out int inverse)
        {
            a = a % n;
            int r1 = n, r2 = a, t1 = 0, t2 = 1, q, r, t;

            while (r2 > 0 && r1 != 0)
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
