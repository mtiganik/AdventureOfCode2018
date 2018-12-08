using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    public delegate void triggerHandler();

    public static class StackClass
    {
        public static List<Tuple<char, char>> initialEntries = new List<Tuple<char, char>>();

        public static List<char> charsInStack = new List<char>();
        public static List<char> result = new List<char>();
        public static int time = 0;

        public static event triggerHandler tickEvent;

        public static void trigger()
        {
            time++;
            tickEvent?.Invoke();
        }

        public static void updateStacks(char doneChar)
        {
            //result.Add(doneChar);
            initialEntries = removeEntriesThatContainedCurrentChar(doneChar, initialEntries);
            updateCharsInStack();
        }
        public static char getNextCharFromStack()
        {
            if (charsInStack.Count > 0)
            {
                int result = charsInStack.Min();
                if (result < 60 || result > 100) return ' ';
                charsInStack.RemoveAll(item => item == (char)result);
                StackClass.result.Add((char)result);
                return (char)result;
            }
            return ' ';
        }

        public static void updateCharsInStack()
        {
            for (int x = 65; x < 91; x++)
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
            return result;
        }


        public static void printCharsInStack()
        {
            foreach (char c in charsInStack)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
        }

        public static void printResult()
        {
            foreach (char c in result)
            {
                Console.Write(c + " ");
            }
        }
    }
}
