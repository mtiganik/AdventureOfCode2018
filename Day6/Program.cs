using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
	class Program
	{
		static List<Tuple<int, int, int>> entries = new List<Tuple<int, int, int>>();
		static int[,] grid = new int[400, 400];
		static void Main(string[] args)
		{
			// get entries from txt file to tuple list
			using (StreamReader sr = new StreamReader("input.txt"))
			{
				string line = "";
				int counter = 0;
				while ((line = sr.ReadLine()) != null)
				{
					entries.Add(new Tuple<int, int, int>(counter, Convert.ToInt32(line.Split(',')[0]), Convert.ToInt32(line.Split(',')[1])));
					counter++;
				}
			}

			// initialize grid
			for (int i = 0; i < 400; i++)
			{
				for (int j = 0; j < 400; j++)
				{
					grid[i, j] = getClosestPointNumberForThisCoordinate(i, j);
				}
			}

			Dictionary<int, int> valuesCount = new Dictionary<int, int>();
			for (int i = 0; i < entries.Count; i++)
			{
				valuesCount.Add(i, isValueInBorder(i) ? -1 : getPointCount(i));
			}

			// prints results for task 1. 
			foreach (var elem in valuesCount)
			{
				Console.WriteLine(elem.Key + "  " + elem.Value);
			}
			int safePointCount = getSafePointCount(10000);
			Console.WriteLine("Safe point count: " + safePointCount);

			Console.ReadLine();
		}


		static int getSafePointCount(int safePointDistance)
		{
			int counter = 0;
			for (int i = 0; i < 400; i++)
			{
				for (int j = 0; j < 400; j++)
				{
					if (isPointSafe(i, j, safePointDistance)) counter++;
				}
			}
			return counter;
		}

		static bool isPointSafe(int x, int y, int safePointValue)
		{
			int safePointCounter = 0;
			for (int i = 0; i < entries.Count; i++)
			{
				safePointCounter += (Math.Abs(x - entries[i].Item2) + Math.Abs(y - entries[i].Item3));
			}
			if (safePointCounter < safePointValue) return true;

			return false;
		}

		static bool isValueInBorder(int coordinateNo)
		{
			for (int i = 0; i < 400; i++)
			{
				if (grid[0, i] == coordinateNo || grid[i, 0] == coordinateNo || grid[399, i] == coordinateNo || grid[i, 399] == coordinateNo)
				{
					return true;
				}
			}
			return false;
		}
		static int getPointCount(int coordinateNo)
		{
			int counter = 0;
			foreach (int point in grid)
			{
				if (point == coordinateNo) counter++;
			}

			return counter;
		}

		static int getClosestPointNumberForThisCoordinate(int x, int y)
		{
			int[] distance = new int[entries.Count];
			for (int i = 0; i < entries.Count; i++)
			{
				distance[i] = (Math.Abs(x - entries[i].Item2) + Math.Abs(y - entries[i].Item3));
			}
			int minDist = distance.Min();
			int minDistCounter = 0;
			int minDistPointNo = 0;
			for (int i = 0; i < distance.Count(); i++)
			{
				if (distance[i] == minDist)
				{
					minDistCounter++;
					minDistPointNo = i;
				}
			}
			if (minDistCounter == 1)
			{
				return minDistPointNo;
			}
			else return -1;
		}
	}
}
