using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor9000
{
    class ColouredSubstring
    {
        public readonly int StartIndex;
        public readonly int Length;
        public readonly ConsoleColor ForegroundColor;
        public readonly ConsoleColor BackgroundColor;

        public ColouredSubstring(int startIndex, int length, ConsoleColor foregroundColor)
        {
            this.StartIndex = startIndex;
            this.Length = length;
            this.ForegroundColor = foregroundColor;
            this.BackgroundColor = Console.BackgroundColor;
        }

        public ColouredSubstring(int startIndex, int length, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            this.StartIndex = startIndex;
            this.Length = length;
            this.ForegroundColor = foregroundColor;
            this.BackgroundColor = backgroundColor;
        }        

    }
}
