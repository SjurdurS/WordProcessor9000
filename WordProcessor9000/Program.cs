using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace WordProcessor9000
{
    internal class Program
    {
        private readonly Parser _parser;
        public String FileContents;

        private Regex _regexDate;
        private Regex _regexUrl;

        private ColoredSubstringList _substrings;


        public Program(String filepath)
        {
            _parser = new Parser();
            FileContents = TextFileReader.ReadFile(filepath);
            InitializeRegexes();

            this._substrings = new ColoredSubstringList(new ColoredSubstring(0, FileContents.Length));
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

            Search(_regexUrl, ConsoleColor.Blue, ConsoleColor.Black);
            Search(_regexDate, ConsoleColor.Red, ConsoleColor.Black);
            ColorPrint();

            while (true)
            {
                ResetSubstrings();
                Command command = _parser.GetCommand();
                ProcessCommand(command);
            }
        }

        private void ResetSubstrings()
        {
            this._substrings = new ColoredSubstringList(new ColoredSubstring(0, FileContents.Length)); // Reset the colored substring list
            Search(_regexUrl, ConsoleColor.Blue, ConsoleColor.Black);
            Search(_regexDate, ConsoleColor.Red, ConsoleColor.Black);
        }

        private void ProcessCommand(Command command)
        {
            String query = command.GetCommandWord();
            if (query == "")
            {
                Console.Clear();
                ColorPrint();
            }
            else if (query.Contains("+") && query.Length > 1)
            {
                query = query.Replace("+", @"\s");
            }
            else if (Regex.Match(query, @"\w\*\b").Success)
            {
                Console.WriteLine("FOund you");
                query = query.Replace("*", @"");
            }
            else
            {
                query = Regex.Escape(query);
            }


            Search(query, ConsoleColor.Black, ConsoleColor.Yellow);


            Console.Clear();
            ColorPrint();
        }

        private void ColorPrint()
        {
            foreach (ColoredSubstring cs in this._substrings)
            {
                string substring = FileContents.Substring(cs.StartIndex, cs.EndIndex - cs.StartIndex);
                ColoredConsoleWrite(substring, cs.BackgroundColor, cs.ForegroundColor);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        private void Search(String query, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            MatchCollection matches = Regex.Matches(FileContents, query);
            int startIndex;
            int endIndex;
            foreach (Match m in matches)
            {
                startIndex = m.Index;
                endIndex = m.Index + m.Length;
                if (endIndex > startIndex) {
                    var cs = new ColoredSubstring(m.Index, m.Index+m.Length, foregroundColor, backgroundColor);
                    this._substrings.Add(cs);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        private void Search(Regex regex, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
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
                    this._substrings.Add(cs);
                }
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

        private void InitializeRegexes()
        {
            // This regex matches the format of the dates in the given example text file:
            // "DDD, dd MMM(M) YYYY" 
            // Where:
            //  DDD is the three letter abbreviation of the days name
            //  dd is one or two digit date
            //  MMM(M) is the abbreviation of the Month
            //  YYYY is either 1xxx or 2xxx.
            _regexDate =
                new Regex(
                    @"(?i:mon|tue|wed|thu|fri|sat|sun),\s\d\d?\s(?i:jan|feb|mar|apr|may|june|july|aug|sept|oct|nov|dec)\s(?:[12]\d{3})");


            // This regex matches all urls of forms:
            //  http(s) or ftp :// address
            // OR
            //  www. address
            // High ASCII characters are also supported. Live example is "www.strøm.dk".
            // Examples:
            //  http://www.feeds.reuters.com/~r/reuters/topNews/~3/ptoAzETqy3w/us-usa-neilarmstrong-idUSBRE87O0B020120825
            //  www.feeds.reuters.com/
            // Allowed characters found here: http://tools.ietf.org/html/rfc3986#appendix-A
            _regexUrl =
                new Regex(
                    @"(?i:https?|ftp)://(?i:[\w+?\.\w+])+(?i:[\w\~\!\@\#\$%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?");
        }


        private static void Main(string[] args)
        {
            var p = new Program(args[0]);
        }
    }
}