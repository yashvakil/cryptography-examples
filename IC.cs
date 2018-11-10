using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class IC
    {
        public static void Main(String[] args)
        {
            Console.Write("Enter string: ");
            char[] str = Console.ReadLine().ToCharArray();
            string distinct = new String(str.Distinct().ToArray());
            double Eprob = 0;

            foreach(char c in distinct)
            {
                int occurence = str.Where(x => x == c).Count();
                double prob = occurence / (double)str.Count();
                Eprob += Math.Pow(prob, 2); 
            }

            Console.WriteLine(Eprob);

            Console.ReadKey();
        }
    }
}
