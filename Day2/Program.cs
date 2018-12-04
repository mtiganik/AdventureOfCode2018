using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
	class Program
	{
		static void Main(string[] args)
		{
			int twoTimesInRow = 0;
			int threeTimesInRow = 0;

			List<string> previousVals = new List<string>();

			using (StreamReader sr = new StreamReader("input.txt"))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					if (containsTwoChars(line))
					{
						twoTimesInRow++;
					}
					if (containtsThreeChars(line))
					{
						threeTimesInRow++;
					}

					foreach(string prev in previousVals)
					{
						if(AreTwoStringsCloselyMatched(line, prev)){
							Console.WriteLine("match:");
							Console.WriteLine(prev);
							Console.WriteLine(line);
						}
					}

					previousVals.Add(line);
				}
			}

			Console.WriteLine("2 times: " + twoTimesInRow);
			Console.WriteLine("3 times in row: " + threeTimesInRow);
			Console.WriteLine("Product: " + (twoTimesInRow * threeTimesInRow));
			Console.ReadKey();
		}

		private static bool AreTwoStringsCloselyMatched(string input1, string input2)
		{
			int counter = 0;
			for (int i = 0; i < input1.Length; i++)
			{
				if (input1[i] != input2[i])
				{
					counter++;
				}
			}
			if (counter == 1) return true;

			return false;
		}

		private static bool containsTwoChars(string input)
		{
			foreach(char c in input)
			{
				if (input.Split(c).Length - 1 == 2) return true;
			}
			return false;
		}

		private static bool containtsThreeChars(string input)
		{
			foreach (char c in input)
			{
				if (input.Split(c).Length - 1 == 3) return true;
			}
			return false;

		}
	}

}
