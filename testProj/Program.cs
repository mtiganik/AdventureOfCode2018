using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testProj
{
	public delegate void triggerHandler();

	public class Program
	{

		static void Main(string[] args)
		{
			using (StreamReader sr = new StreamReader("input.txt"))
			{
				string line = "";
				while ((line = sr.ReadLine()) != null)
				{
					StackClass.initialEntries.Add(new Tuple<char, char>(line.Split(' ')[1].SingleOrDefault(), line.Split(' ')[7].SingleOrDefault()));
				}

				StackClass.updateCharsInStack();
				StackClass.printCharsInStack();

				Worker w1 = new Worker();
				Worker w2 = new Worker();
				w1.CurrentChar = StackClass.getNextCharFromStack();
				w2.CurrentChar = StackClass.getNextCharFromStack();

				StackClass.tickEvent += new triggerHandler(() => w1.tickEventHappened());
				StackClass.tickEvent += new triggerHandler(() => w2.tickEventHappened());
				int i = 0;
				while( StackClass.initialEntries.Count > 0 || StackClass.charsInStack.Count > 0){
					StackClass.trigger();
					Thread.Sleep(5);
					i++;
				}
				Console.WriteLine("It tooked time: " + StackClass.time);
				Console.ReadLine();
			}
		}
	}

	public class Worker
	{
		private char currentChar;
		public char CurrentChar { get { return currentChar; } set { currentChar = value; setTimeForThisChar(value); } }
		public int timeForChar;

		int timer = 0; // for debbugging purpose

		public void tickEventHappened()
		{
			timer++;
			if(currentChar == ' ')
			{
				if(StackClass.charsInStack.Count > 0)
				{
					this.CurrentChar = StackClass.getNextCharFromStack();
				}
			}
			else
			{
				timeForChar--;
				if (timeForChar <= 0)
				{
					StackClass.sendCurrentCharToResultStack(currentChar);
					Console.WriteLine("Sent char to stack");

					if (StackClass.charsInStack.Count > 0)
					{
						this.CurrentChar = StackClass.getNextCharFromStack();
						Console.WriteLine("got new char: " + this.CurrentChar);
					}

				}

			}
			Console.WriteLine("curr char: " + currentChar + " time for this: " + timeForChar);
		}

		private void setTimeForThisChar(char value)
		{
			this.timeForChar = (int)value - 64;
		}
	}

	public static class StackClass
	{
		public static List<Tuple<char, char>> initialEntries = new List<Tuple<char, char>>();

		public static List<char> charsInStack = new List<char>();
		static List<char> result = new List<char>();
		public static int time = 0;

		public static event triggerHandler tickEvent;

		public static void trigger()
		{
			time++;
			tickEvent?.Invoke();
		}

		public static void sendCurrentCharToResultStack(char doneChar)
		{
			result.Add(doneChar);
			initialEntries = removeEntriesThatContainedCurrentChar(doneChar, initialEntries);
			updateCharsInStack();
		}
		public static char getNextCharFromStack()
		{
			if(charsInStack.Count > 0)
			{
				int result = charsInStack.Min();
				if (result < 60 || result > 100) return ' ';
				charsInStack.Remove((char)result);
				return (char)result;
			}
			return ' ';
		}

		public static void updateCharsInStack()
		{
			for(int x = 65; x < 71; x++)
			{
				if (result.Contains((char)x) || charsInStack.Contains((char)x)) continue;
				if (initialEntries.Where(u => u.Item2.Equals((char)x)).Count() == 0) charsInStack.Add((char)x);
			}
		}

		static List<Tuple<char, char>> removeEntriesThatContainedCurrentChar(char charToRemove, List<Tuple<char, char>> entries)
		{
			List<Tuple<char, char>> result = new List<Tuple<char, char>>();
			foreach (var item in entries)
			{
				if (!item.Item1.Equals(charToRemove)) result.Add(item);
			}
			//entries.Where(u => u.Item1.Equals(charToRemove)).
			return result;
		}


		public static void printCharsInStack()
		{
			foreach(char c in charsInStack)
			{
				Console.Write(c + " ");
			}
			Console.WriteLine();
		}
	}
}
