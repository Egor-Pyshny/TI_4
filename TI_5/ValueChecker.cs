using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TI_5
{
    public class ValueChecker
    {
        //тест Миллера-Рабина для проверки на простоту
        public static bool IsPrime(BigInteger number, int rounds)
        {
            if (number < 2)
            {
                return false;
            }

            if (number == 2 || number == 3)
            {
                return true;
            }

            if (number % 2 == 0)
            {
                return false;
            }

            BigInteger d = number - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            Random random = new Random();

            for (int i = 0; i < rounds; i++)
            {
                BigInteger a = BigInteger.Remainder(BigInteger.Subtract(number, 2), number);
                a = BigInteger.Add(a, random.Next(2, int.MaxValue));

                BigInteger x = BigInteger.ModPow(a, d, number);

                if (x == 1 || x == number - 1)
                {
                    continue;
                }

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, number);

                    if (x == number - 1)
                    {
                        break;
                    }

                    if (r == s - 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //проверка q
        public static void checkQ(BigInteger q)
		{
			if (!IsPrime(q,95))
			{
				throw new Exception();
			}
		}

        //проверка P
		public static void checkP(BigInteger p,BigInteger q)
		{
			if (!IsPrime(q, 95))
			{
				throw new Exception();
			}

			if ((p - BigInteger.One) % q != BigInteger.Zero)
			{
				throw new Exception();
			}
		}


        //проверка H
		public static void checkH(BigInteger q,BigInteger p,BigInteger h)
		{
			if (h < new BigInteger(2) || h > (p - new BigInteger(2)))
			{
				throw new Exception();
			}
            BigInteger temp = (p - BigInteger.One) / q;
            var g = BigInteger.ModPow(h, temp, p);

            if (g <= BigInteger.One){
				throw new Exception();
			}
		}

        //проверка попадания в заданный интервал
		public static void checkInterval(BigInteger leftBound,BigInteger rightBound,BigInteger value)
		{
			if (value < leftBound || value > rightBound)
			{
				throw new Exception();
			}
		}
	}
}
