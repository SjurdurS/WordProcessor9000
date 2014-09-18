using System;

namespace WordProcessor9000
{
    /// <summary>
    ///     This class represents the indices of a substring of another string
    ///     and the ConsoleColors the substring should use.
    /// </summary>
    internal class ColoredSubstring
    {
        public readonly ConsoleColor BackgroundColor;
        public readonly int EndIndex;
        public readonly ConsoleColor ForegroundColor;
        public readonly int StartIndex;

        /// <summary>
        ///     Instantiate a new ColoredSubstring with default foreground and background colors.
        /// </summary>
        /// <param name="startIndex">Starting index of the substring</param>
        /// <param name="endIndex">Ending index of the substring</param>
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
        ///     Instantiate a new ColoredSubstring with a specified foreground color and default background color.
        /// </summary>
        /// <param name="startIndex">Starting index of the substring</param>
        /// <param name="endIndex">Ending index of the substring</param>
        /// <param name="foregroundColor">The color used as foreground color</param>
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
        ///     Instantiate a new ColoredSubstring with a specified foreground and background color.
        /// </summary>
        /// <param name="startIndex">Starting index of the substring</param>
        /// <param name="endIndex">Ending index of the substring</param>
        /// <param name="foregroundColor">The color used as foreground color</param>
        /// <param name="backgroundColor">The color used as background color</param>
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

        public override string ToString()
        {
            return (StartIndex.ToString().PadLeft(6) + ":" + EndIndex.ToString().PadLeft(6) + ":" +
                    ForegroundColor.ToString().PadLeft(12) + ":" + BackgroundColor.ToString().PadLeft(12));
        }
    }
}