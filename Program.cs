using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigInteger
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger bi1, bi2;
            bi1 = new BigInteger();
            bi2 = new BigInteger();
            bi1 = 23; //implicit cast from int to BigInteger
            bi2 = 232; //
            Console.WriteLine("bi1 = " + bi1);
            Console.WriteLine("bi2 = " + bi2);
            BigInteger bi3 = new BigInteger();
            bi3 = bi1 * -3; 
            Console.WriteLine("bi3 = bi1 * -3 = " + bi3);
            int number = (int)bi1; //explicit cast from BigInteger to Int32
            Console.WriteLine("Int32 number = bi1 = " + number);
            Console.WriteLine("bi1 - bi2 = " + (bi1 - bi2));
            Console.WriteLine("bi1 + bi2 = " + (bi1 + bi2));

            BigInteger bi4 = Int32.MaxValue;
            Console.WriteLine("Int32.MaxValue * 2 = " + (bi4 * 2));
         }
    }
}
