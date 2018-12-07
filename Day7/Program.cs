using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
	class Program
	{
		static void Main(string[] args)
		{
			 List<Tuple<char, char>> entries = new List<Tuple<char, char>>();
			 

			using (StreamReader sr = new StreamReader("input.txt"))
			{
				string line = "";
				while ((line = sr.ReadLine()) != null)
				{
					entries.Add(new Tuple<char, char>(line.Split(' ')[1].SingleOrDefault(), line.Split(' ')[7].SingleOrDefault()));
				}
			}

			string Part1Result = GetPartOneString(entries);
			Console.WriteLine(Part1Result);
			Console.ReadLine();
		}

		class Worker
		{
			public char currentChar { get { return currentChar; } set { currentChar = value; setTimeForThisChar(value); } }
			public int timeForChar;

			private void setTimeForThisChar(char value)
			{
				this.timeForChar = (int)value - 4;
			}

		}

		private static string GetPartOneString(List<Tuple<char, char>> entries)
		{
			int counter = 0;
			char next = ' ';
			char[] result = new char[40];
			while (entries.Count >= 0 && next != '*')
			{
				next = getNextCharValue(result, entries);
				result[counter] = next;
				counter++;
				entries = removeEntriesThatContainedCurrentChar(next, entries);
				Console.WriteLine(next);
			}
			return new string(result);
		}

		static List<Tuple<char, char>> removeEntriesThatContainedCurrentChar(char charToRemove, List<Tuple<char, char>> entries)
		{
			List<Tuple<char, char>> result = new List<Tuple<char, char>>();
			foreach(var item in entries)
			{
				if (!item.Item1.Equals(charToRemove)) result.Add(item);
			}
			//entries.Where(u => u.Item1.Equals(charToRemove)).
			return result;
		}

		static char getNextCharValue(char[] result, List<Tuple<char, char>> entries)
		{

			for(int x = 65; x < 91; x++)
			{
				if (result.Contains((char)x)) continue;
				if (entries.Where(u => u.Item2.Equals((char)x)).Count() == 0) return (char)x;
			}

			// Indication that loop has ended
			return '*';

		}
	}
}
