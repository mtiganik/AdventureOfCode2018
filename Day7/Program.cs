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
        public static List<Worker> workers = new List<Worker>();

        static void Main(string[] args)
        {
            List<Tuple<char, char>> entries = new List<Tuple<char, char>>();


            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    // same lists for 2 tasks
                    entries.Add(new Tuple<char, char>(line.Split(' ')[1].SingleOrDefault(), line.Split(' ')[7].SingleOrDefault()));
                    StackClass.initialEntries.Add(new Tuple<char, char>(line.Split(' ')[1].SingleOrDefault(), line.Split(' ')[7].SingleOrDefault()));
                }
            }

            // Part 2
            StackClass.updateCharsInStack();
            for (int x = 0; x < 5; x++)
            {
                Worker worker = new Worker();
                StackClass.tickEvent += new triggerHandler(() => worker.tickEventHappened());
                workers.Add(worker);
            }

            int i = 0;
            while (StackClass.initialEntries.Count > 0 || StackClass.charsInStack.Count > 0 || !AreAllWorkersFinished())
            {

                StackClass.trigger();
                Console.WriteLine("{0} [{1}]", i, getCurrentWorkersChars());
                i++;

            }
            Console.WriteLine("Part 2 time: " + (StackClass.time - 1) + " " + (i - 2));

            // Part 1
            Console.WriteLine("Part 1 result:");
            string Part1Result = GetPartOneString(entries);
            Console.WriteLine(Part1Result);


            Console.ReadLine();
        }


        static bool AreAllWorkersFinished()
        {
            foreach (Worker w in workers)
            {
                if (w.CurrentChar != ' ') return false;
            }
            return true;
        }

        static string getCurrentWorkersChars()
        {
            string result = "";
            foreach (Worker w in workers)
            {
                result += w.CurrentChar + " ";
            }

            return result;
        }



        // task 1 methods
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
