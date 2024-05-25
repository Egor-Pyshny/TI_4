using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TI_5
{
    public class Hash
    {

        //хэширует по формуле
        public static BigInteger get_hash(string input, BigInteger mod) {
            BigInteger res = new BigInteger(100);
            
            for (int i = 0; i < input.Length; i++) {
                var tmp = BigInteger.Pow(res + new BigInteger(input[i]), 2);
                res = tmp % mod;
            }
            return res;
        }
    }
}
