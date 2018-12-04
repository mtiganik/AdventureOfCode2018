using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
	class Program
	{

		static void Main(string[] args)
		{
			int numOfMatches = 0;
			int[,] intArr = new int[1200, 1200];


			using (StreamReader sr = new StreamReader("input.txt"))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					line = line.Split('@')[1];
					int rowStart = Convert.ToInt32(line.Split(',')[0]);
					int columnStart = Convert.ToInt32((line.Split(':')[0]).Split(',')[1]);
					line = line.Split(':')[1];
					int rowLength = Convert.ToInt32(line.Split('x')[0]);
					int columnLength = Convert.ToInt32(line.Split('x')[1]);
					for(int i = rowStart; i < rowStart + rowLength; i++)
					{
						for(int j = columnStart; j < columnStart + columnLength; j++)
						{
							intArr[i,j]++;
						}
					}
				}

			}

			using (StreamReader sr = new StreamReader("input.txt"))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					int lineIndex = Convert.ToInt32(line.Split('@')[0].TrimStart('#'));
					line = line.Split('@')[1];

					int rowStart = Convert.ToInt32(line.Split(',')[0]);
					int columnStart = Convert.ToInt32((line.Split(':')[0]).Split(',')[1]);
					line = line.Split(':')[1];
					int rowLength = Convert.ToInt32(line.Split('x')[0]);
					int columnLength = Convert.ToInt32(line.Split('x')[1]);

					if(NoOverlapsForThisClaim(rowStart,rowLength,columnStart, columnLength)){
						Console.WriteLine("No overlap for index: "+ lineIndex);
					}
				}

			}

			for (int i = 0; i < 1200; i++)
			{
				for (int j = 0; j < 1200; j++)
				{
					if(intArr[i,j] > 1)
					{
						numOfMatches++;
					}
				}
			}
			Console.WriteLine("Number of matches: " + numOfMatches);
			Console.ReadKey();



			bool NoOverlapsForThisClaim(int rowStart, int rowLength, int columnStart, int columnLength)
			{
				for (int i = rowStart; i < rowStart + rowLength; i++)
				{
					for (int j = columnStart; j < columnStart + columnLength; j++)
					{
						if (intArr[i, j] != 1) return false;
					}
				}
				return true;
			}
		}
	}
}
