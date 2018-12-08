using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    public class Worker
    {
        private char currentChar = ' ';
        public char CurrentChar { get { return currentChar; } set { currentChar = value; setTimeForThisChar(value); } }
        public int timeForChar;

        int timer = 0; // for debbugging purpose

        public void tickEventHappened()
        {
            timer++;
            if (currentChar == ' ')
            {
                if (StackClass.charsInStack.Count > 0)
                {
                    this.CurrentChar = StackClass.getNextCharFromStack();
                }
            }
            else
            {
                timeForChar--;
                if (timeForChar <= 0)
                {
                    StackClass.updateStacks(currentChar);

                    if (StackClass.charsInStack.Count > 0)
                    {
                        this.CurrentChar = StackClass.getNextCharFromStack();
                    }
                    else
                    {
                        this.CurrentChar = ' ';
                    }

                }

            }
        }

        private void setTimeForThisChar(char value)
        {
            if (value == ' ') this.timeForChar = 0;
            else
            {
                this.timeForChar = (int)value - 4;
            }
        }
    }
}
