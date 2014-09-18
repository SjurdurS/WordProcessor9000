﻿using System;
using System.Text.RegularExpressions;

namespace WordProcessor9000
{
    internal class Program
    {
        private readonly Parser _parser;
        public String FileContents;

        /// <summary>
        /// List containing indices over substrings in the FileContents string and what color each should be drawn with.
        /// </summary>
        private ColoredSubstringList _substrings;

        /// <summary>
        /// Instantiate the program.
        /// </summary>
        /// <param name="filepath"></param>
        public Program(String filepath)
        {
            _parser = new Parser();
            FileContents = TextFileReader.ReadFile(filepath);

            _substrings = new ColoredSubstringList(new ColoredSubstring(0, FileContents.Length));
            Start();
        }

        private void PrintWelcome()
        {
            Console.Clear();
            Console.WriteLine("Welcome to WordProcessor9000\n");
            Console.WriteLine("This software allows you to search for and highlight words in a given text.");
            Console.WriteLine("Available commands:");
            Console.WriteLine("+".PadRight(10) + "Two adjacent keywords are followed one after the other:");
            Console.WriteLine("".PadRight(10) + "e.g. 'text + file' matches 'text file'.");
            Console.WriteLine("*".PadRight(10) + "Search for words starting with or ending with a keyword:");
            Console.WriteLine("".PadRight(10) + "e.g. '*net' matches 'internet'");
            Console.WriteLine("".PadRight(10) + "e.g. 'fash*' matches 'fashion'");
            Console.WriteLine();
            Console.WriteLine("CTRL+C to quit at any time.\n");
        }

        public void Start()
        {
            PrintWelcome();

            Console.WriteLine("Press any key to begin.");
            Console.ReadKey();
            Console.Clear();

            Search(CompiledRegexes.URL, ConsoleColor.Blue, ConsoleColor.Black);
            Search(CompiledRegexes.Date, ConsoleColor.Red, ConsoleColor.Black);
            ColorPrint();

            while (true)
            {
                ResetSubstrings();
                String userInput = _parser.GetUserInput();
                ProcessUserInput(userInput);
            }
        }

        private void ResetSubstrings()
        {
            _substrings = new ColoredSubstringList(new ColoredSubstring(0, FileContents.Length));
                // Reset the colored substring list
            Search(CompiledRegexes.URL, ConsoleColor.Blue, ConsoleColor.Black);
            Search(CompiledRegexes.Date, ConsoleColor.Red, ConsoleColor.Black);
        }



        private void ProcessUserInput(String userInput)
        {
            String query = userInput;
            if (query == "")
            {
                Console.Clear();
                ColorPrint();
                return;
            }

            // Escape the query to make sure the user cannot use other special characters
            query = Regex.Escape(query);

            // Unescape our available commands.
            query = Regex.Replace(query, @"\\\*", "*");
            query = Regex.Replace(query, @"\\\+", "+");

            try
            {
                // Words that start with the query
                query = Regex.Replace(query, CompiledRegexes.StartsWith.ToString(), CompiledRegexes.StartsWithReplacement.ToString());


                // Words that end with the query
                query = Regex.Replace(query, CompiledRegexes.EndsWith.ToString(), CompiledRegexes.EndsWithReplacement.ToString());

            }
            catch (ArgumentException ex)
            {
                // Do nothing
            }

            if (query.Contains("+") && query.Length > 1)
            {
                query = query.Replace("+", @"\s");
            }

            Search(query, ConsoleColor.Black, ConsoleColor.Yellow);


            Console.Clear();
            ColorPrint();
        }

        private void ColorPrint()
        {
            foreach (ColoredSubstring cs in _substrings)
            {
                string substring = FileContents.Substring(cs.StartIndex, cs.EndIndex - cs.StartIndex);
                ColoredConsoleWrite(substring, cs.BackgroundColor, cs.ForegroundColor);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        private void Search(String query, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            try
            {
                MatchCollection matches = Regex.Matches(FileContents, query);
                int startIndex;
                int endIndex;
                foreach (Match m in matches)
                {
                    startIndex = m.Index;
                    endIndex = m.Index + m.Length;
                    if (endIndex > startIndex)
                    {
                        var cs = new ColoredSubstring(m.Index, m.Index + m.Length, foregroundColor, backgroundColor);
                        _substrings.Add(cs);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Do nothing
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        private void Search(Regex regex, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            try
            {
                MatchCollection matches = regex.Matches(FileContents);
                int startIndex;
                int endIndex;
                foreach (Match m in matches)
                {
                    startIndex = m.Index;
                    endIndex = m.Index + m.Length;
                    if (endIndex > startIndex)
                    {
                        var cs = new ColoredSubstring(m.Index, m.Index + m.Length, foregroundColor, backgroundColor);
                        _substrings.Add(cs);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Do nothing
            }
        }


        /// <summary>
        ///     Writes a string to the console with the specified font color.
        /// </summary>
        /// <param name="str">The string to color</param>
        /// <param name="color">The font color</param>
        private static void ColoredConsoleWrite(String str, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        ///     Writes a string to the console with the specified background / foreground colors.
        /// </summary>
        /// <param name="str">The string to color</param>
        /// <param name="backgroundColor">The background color</param>
        /// <param name="foregroundColor">The font color</param>
        private static void ColoredConsoleWrite(String str, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(str);
            Console.BackgroundColor = originalBackgroundColor;
            Console.ForegroundColor = originalForegroundColor;
        }


        private static void Main(string[] args)
        {
            var p = new Program(args[0]);
        }
    }
}