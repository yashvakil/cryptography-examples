using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public Point() { }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class ECCElgamal
    {
        public static Random random = new Random();
        public static void Main(String[] args)
        {
            
            Console.Write("Enter a:  ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter b:  ");
            int b = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter p:  ");
            int p = Convert.ToInt32(Console.ReadLine());

            int temp = (int)((4 * Math.Pow(a, 3)) + (27 * Math.Pow(b, 2)));

            if (temp == 0)
            {
                Console.WriteLine("Enter Proper Numbers");
                System.Environment.Exit(1);
            }

            List<Point> points = getPoints(a, b, p);

            Point e1 = points[random.Next(points.Count)];
            Point e2 = e1;
            Point pt = points[random.Next(points.Count)];
            
            int d = random.Next(2, p - 1);
            int d1 = d;
            bool flag = false;
            if (d1 % 2 != 0)
            {
                d1--;
                flag = true;
            }
            while (d1 > 1)
            {
                e2 = doublePoint(e2, a, b, p);
                d1 = d1 / 2;
            }
            if (flag)
                e2 = addPoint(e1, e2, p);

            Console.WriteLine("\n\n");
            Console.WriteLine("Public Key: {0} {1} {2} ({3}, {4}) ({5}, {6})", a, b, p, e1.x, e1.y, e2.x, e2.y);
            Console.WriteLine("Private Key: {0}", d);

            List<Point> encoded = Encode(pt, e1, e2, a, b, p);
            Point decoded = Decode(encoded, d, a, b, p);

            Console.WriteLine("\n\n");
            Console.WriteLine("Plain Text: ({0}, {1})", pt.x, pt.y);
            Console.WriteLine("Encoded Text: ({0}, {1})  ({2}, {3})", encoded[0].x, encoded[0].y, encoded[1].x, encoded[1].y);
            Console.WriteLine("Decoded Text: ({0}, {1})", decoded.x, decoded.y);

            Console.ReadKey();
        }

        public static List<Point> Encode(Point pt, Point e1, Point e2, int a, int b,int p)
        {
            List<Point> points = new List<Point>();
            Point c1 = e1;
            Point c2 = e2;

            int r = random.Next(2, p - 1);
            
            bool flag = false;
            if (r % 2 != 0)
            {
                r--;
                flag = true;
            }
            while (r > 1)
            {
                c1 = doublePoint(c1, a, b, p);
                c2 = doublePoint(c2, a, b, p);
                r = r / 2;
            }
            if (flag)
            {
                c1 = addPoint(e1, c1, p);
                c2 = addPoint(e2, c2, p);
            }
            c2 = addPoint(pt, c2, p);

            points.Add(c1);
            points.Add(c2);

            return points;
        }

        public static Point Decode(List<Point> encoded, int d, int a, int b, int p)
        {
            Point pt = encoded[0];
            bool flag = false;
            if (d % 2 != 0)
            {
                d--;
                flag = true;
            }
            while (d > 1)
            {
                pt = doublePoint(pt, a, b, p);
                d = d / 2;
            }
            if (flag)
                pt = addPoint(encoded[0], pt, p);

            pt.y = -1 * pt.y;
            while (pt.y < 0)
                pt.y += p;

            pt = addPoint(encoded[1], pt, p);
            return pt;
        }
        public static Point addPoint(Point p1, Point p2, int p)
        {
            Point pi = new Point();
            int lambdaTop = ((int)( p2.y - p2.y ))% p;
            int lambdaBottom = ((int)(p2.x - p2.x)) % p;
            while (lambdaTop < 0)
                lambdaTop += p;
            lambdaTop = lambdaTop % p;
            while (lambdaBottom < 0)
                lambdaBottom += p;
            lambdaBottom = lambdaBottom % p;

            int gcd;
            Euclidean(lambdaBottom, p, out gcd, out lambdaBottom);
            int lambda = (lambdaBottom * lambdaTop) % p;

            pi.x = (int)(Math.Pow(lambda, 2) - p1.x - p2.x );
            while (pi.x < 0)
                pi.x += p;
            pi.x = pi.x % p;

            pi.y = (int)(lambda * (p1.x - pi.x) - p1.y);
            while (pi.y < 0)
                pi.y += p;
            pi.y = pi.y % p;

            return pi;
        }

        public static Point doublePoint(Point p1, int a, int b, int p)
        {
            Point pi = new Point();
            int lambdaTop = ((int)(3 * Math.Pow(p1.x, 2) + a)) % p;
            int lambdaBottom = (int)(2 * p1.y);
            int gcd;
            Euclidean(lambdaBottom, p, out gcd, out lambdaBottom);
            int lambda = (lambdaBottom * lambdaTop) % p;

            pi.x = (int)(Math.Pow(lambda, 2) - (2 * p1.x));
            while (pi.x < 0)
                pi.x += p;
            pi.x = pi.x % p;

            pi.y = (int)(lambda * (p1.x - pi.x) - p1.y);
            while (pi.y < 0)
                pi.y += p;
            pi.y = pi.y % p;

            return pi;
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

        public static List<Point> getPoints(int a, int b, int p)
        {
            List<Point> points = new List<Point>();
            int x = 0;
            while (x < p)
            {
                int w = (int)((Math.Pow(x, 3) + (a * x) + b) % p);
                while(w < p * p)
                {
                    if (Math.Sqrt(w) % 1 == 0)
                    {
                        points.Add(new Point(x, (int)Math.Sqrt(w)));
                        points.Add(new Point(x, (int)(-1 * Math.Sqrt(w)) + p));
                        break;
                    }
                    else
                        w += p;
                }
                x++;
            }
            return points;
        }
    }

}
