using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor9000
{
    class ColoredSubstring
    {
        public readonly int StartIndex;
        public readonly int EndIndex;
        public readonly ConsoleColor ForegroundColor;
        public readonly ConsoleColor BackgroundColor;

        public ColoredSubstring(int startIndex, int endIndex)
        {
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
            if (this.StartIndex >= this.EndIndex)
            {
                throw new IndexOutOfRangeException("EndIndex has to be greater than startIndex");
            }
            this.ForegroundColor = Console.ForegroundColor;
            this.BackgroundColor = Console.BackgroundColor;
        }
        public ColoredSubstring(int startIndex, int endIndex, ConsoleColor foregroundColor)
        {
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
            if (this.StartIndex >= this.EndIndex)
            {
                throw new IndexOutOfRangeException("EndIndex has to be greater than startIndex");
            }
            this.ForegroundColor = foregroundColor;
            this.BackgroundColor = Console.BackgroundColor;
        }

        public ColoredSubstring(int startIndex, int endIndex, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
            if (this.StartIndex >= this.EndIndex)
            {
                throw new IndexOutOfRangeException("EndIndex has to be greater than startIndex");
            }
            this.ForegroundColor = foregroundColor;
            this.BackgroundColor = backgroundColor;
        }

        public void PrintMe() {

            Console.WriteLine(this.StartIndex.ToString().PadLeft(5) + ":" + this.EndIndex.ToString().PadLeft(5) + ":" + this.ForegroundColor.ToString().PadLeft(8) + ":" + this.BackgroundColor.ToString().PadLeft(8));
        }

    }
}
