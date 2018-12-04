using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
	class Program
	{
		static void Main(string[] args)
		{
			int freqValue = 0;
			List<int> previousVals = new List<int>();
			bool hasMatchFound = false;

			while (!hasMatchFound)
			{
				try
				{
					using (StreamReader sr = new StreamReader("input.txt"))
					{
						string line;
						while ((line = sr.ReadLine()) != null)
						{
							freqValue += Convert.ToInt32(line);
							foreach (int prevs in previousVals)
							{
								if (prevs == freqValue)
								{
									Console.WriteLine("Found match: " + freqValue);
									hasMatchFound = true;
									break;
								}
							}
							previousVals.Add(freqValue);


						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("Exception " + e);
				}
				Console.WriteLine("endVal: " + freqValue);
			}
			Console.WriteLine(freqValue);
			Console.ReadKey();
		}
	}
}
