using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TI_5
{
    public class Signer
    {
		private String inputPath;
		private String outputPath;
		private BigInteger q;
		private BigInteger p;
		private BigInteger h;
		private BigInteger k;
		private BigInteger m;

		public Signer(BigInteger in_q, BigInteger in_p, BigInteger in_h, BigInteger in_k, BigInteger in_m, string inp, string outp) {
			this.inputPath = inp;
			this.outputPath = outp;
			this.q = in_q;
			this.p = in_p;
			this.h = in_h;
			this.k = in_k;
			this.m = in_m;
		}

		//здесь происходит подписывание
		public BigInteger[] ensign(BigInteger x) {

			//вычислительный блок
			string msg = File.ReadAllText(inputPath).ToUpper();
			BigInteger hash = Hash.get_hash(msg, m);
			BigInteger temp = (p - BigInteger.One) / q;
			var g = BigInteger.ModPow(h, temp, p);
			var y = BigInteger.ModPow(g, x, p);
			BigInteger temp1 = BigInteger.ModPow(g, k, p);
			var r = temp1 % q;
			BigInteger temp2 = BigInteger.Pow(k, (int)q - 2);
			temp2 = temp2 * (hash + x * r);
			var s = temp2 % q;

			//если r или s нулевые ничего не произойдет
			if (r != BigInteger.Zero && s != BigInteger.Zero) {

				//запись результата
				StringBuilder sb = new StringBuilder();
				sb.Append(msg);
				sb.Append(";;");
				sb.Append($"({r}, {s})");
				File.WriteAllText(outputPath,sb.ToString());
			}
			return new BigInteger[4] { hash, r, s, y };
		}

		//здесь происходит проверка
		public BigInteger[] design(BigInteger y) {
			string msg = File.ReadAllText(inputPath).ToUpper();
			int i = msg.Length-1;
			StringBuilder str = new StringBuilder();

			//отделение подписи от текста
			while (msg[i] != ';') {
				str.Insert(0,msg[i]);
				i--;
			}
			i--;
			msg = msg.Remove(i, msg.Length - i);
			str.Replace(" ", "").Replace("(","").Replace(")","");
			string temp = str.ToString();

			string[] arr = temp.Split(',');

			//вычислительный блок
			BigInteger r = BigInteger.Parse(arr[0]);
			BigInteger s = BigInteger.Parse(arr[1]);
			BigInteger hash = Hash.get_hash(msg, m);
			BigInteger temp1 = (p - BigInteger.One) / q;
			BigInteger g = BigInteger.ModPow(h, temp1, p);
			BigInteger w = BigInteger.ModPow(s, q - new BigInteger(2), q);
			BigInteger u1 = (hash * w) % q;
			BigInteger u2 = (r * w) % q;
			BigInteger temp2 = BigInteger.Pow(g, (int)u1);
			BigInteger temp3 = BigInteger.Pow(y, (int)u2);
			BigInteger temp4 = (temp2 * temp3) % p;
			BigInteger v = temp4 % q;

			//проверка совпадения
			if (r == v)
			{
				File.WriteAllText(outputPath, msg);
			}

			return new BigInteger[7] {hash, r, s, w, u1, u2, v};
		}
	}
}
