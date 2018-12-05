using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
	class Program
	{
		static void Main(string[] args)
		{
			string line = "";
			using (StreamReader sr = new StreamReader("input.txt"))
			{
				line = sr.ReadLine();

			}
			Console.WriteLine(line.Length);
			int lineLength = GetReactionLength(line);



			for(int i = 97; i < 123; i++)
			{
				char smallLetter = (char)i;
				char bigLetter = (char)(i - 32);
				string subLine = line.Replace(smallLetter.ToString(), "").Replace(bigLetter.ToString(), "");
				
				Console.WriteLine(smallLetter + " " + GetReactionLength(subLine));
			}

			Console.ReadKey();
		}

		private static int GetReactionLength(string line)
		{
			int previousCount = -1;
			int currCount = 0;


			while (previousCount != currCount)
			{
				previousCount = line.Length;
				line = line.Replace("aA", "");
				line = line.Replace("Aa", "");
				line = line.Replace("bB", "");
				line = line.Replace("Bb", "");
				line = line.Replace("cC", "");
				line = line.Replace("Cc", "");
				line = line.Replace("dD", "");
				line = line.Replace("Dd", "");
				line = line.Replace("eE", "");
				line = line.Replace("Ee", "");
				line = line.Replace("fF", "");
				line = line.Replace("Ff", "");
				line = line.Replace("gG", "");
				line = line.Replace("Gg", "");
				line = line.Replace("hH", "");
				line = line.Replace("Hh", "");
				line = line.Replace("iI", "");
				line = line.Replace("Ii", "");
				line = line.Replace("jJ", "");
				line = line.Replace("Jj", "");
				line = line.Replace("kK", "");
				line = line.Replace("Kk", "");
				line = line.Replace("lL", "");
				line = line.Replace("Ll", "");
				line = line.Replace("mM", "");
				line = line.Replace("Mm", "");
				line = line.Replace("nN", "");
				line = line.Replace("Nn", "");
				line = line.Replace("oO", "");
				line = line.Replace("Oo", "");
				line = line.Replace("pP", "");
				line = line.Replace("Pp", "");
				line = line.Replace("qQ", "");
				line = line.Replace("Qq", "");
				line = line.Replace("rR", "");
				line = line.Replace("Rr", "");
				line = line.Replace("sS", "");
				line = line.Replace("Ss", "");
				line = line.Replace("tT", "");
				line = line.Replace("Tt", "");
				line = line.Replace("uU", "");
				line = line.Replace("Uu", "");
				line = line.Replace("vV", "");
				line = line.Replace("Vv", "");
				line = line.Replace("wW", "");
				line = line.Replace("Ww", "");
				line = line.Replace("xX", "");
				line = line.Replace("Xx", "");
				line = line.Replace("yY", "");
				line = line.Replace("Yy", "");
				line = line.Replace("zZ", "");
				line = line.Replace("Zz", "");
				currCount = line.Length;
			}

			return currCount;
		}
	}
}
