using System;

namespace WordProcessor9000
{
    /// <summary>
    /// This class represents the indices of a substring of another string
    /// and the ConsoleColors the substring should use.
    /// </summary>
    internal class ColoredSubstring
    {
        public readonly ConsoleColor BackgroundColor;
        public readonly int EndIndex;
        public readonly ConsoleColor ForegroundColor;
        public readonly int StartIndex;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="foregroundColor"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
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

        /// <summary>
        /// Write the contents of this string to the console.
        /// </summary>
        public override string ToString()
        {
            return (StartIndex.ToString().PadLeft(6) + ":" + EndIndex.ToString().PadLeft(6) + ":" +
                              ForegroundColor.ToString().PadLeft(12) + ":" + BackgroundColor.ToString().PadLeft(12));
        }
    }
}