using System;

namespace WordProcessor9000
{
    internal class ColoredSubstring
    {
        public readonly ConsoleColor BackgroundColor;
        public readonly int EndIndex;
        public readonly ConsoleColor ForegroundColor;
        public readonly int StartIndex;

        public ColoredSubstring(int startIndex, int endIndex)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            if (StartIndex >= EndIndex)
            {
                throw new IndexOutOfRangeException("EndIndex has to be greater than startIndex");
            }
            ForegroundColor = Console.ForegroundColor;
            BackgroundColor = Console.BackgroundColor;
        }

        public ColoredSubstring(int startIndex, int endIndex, ConsoleColor foregroundColor)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            if (StartIndex >= EndIndex)
            {
                throw new IndexOutOfRangeException("EndIndex has to be greater than startIndex");
            }
            ForegroundColor = foregroundColor;
            BackgroundColor = Console.BackgroundColor;
        }

        public ColoredSubstring(int startIndex, int endIndex, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            if (StartIndex >= EndIndex)
            {
                throw new IndexOutOfRangeException("EndIndex has to be greater than startIndex");
            }
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public void PrintMe()
        {
            Console.WriteLine(StartIndex.ToString().PadLeft(5) + ":" + EndIndex.ToString().PadLeft(5) + ":" +
                              ForegroundColor.ToString().PadLeft(8) + ":" + BackgroundColor.ToString().PadLeft(8));
        }
    }
}