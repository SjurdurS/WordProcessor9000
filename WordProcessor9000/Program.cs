using System;
using System.Text.RegularExpressions;

namespace WordProcessor9000
{
    internal class Program
    {
        private readonly Parser _parser;
        public String FileContents;

        private Regex _regexDate;
        private Regex _regexEndsWith;
        private Regex _regexStartsWith;
        private Regex _regexUrl;

        private ColoredSubstringList _substrings;


        public Program(String filepath)
        {
            _parser = new Parser();
            FileContents = TextFileReader.ReadFile(filepath);
            InitializeRegexes();

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

            Search(_regexUrl, ConsoleColor.Blue, ConsoleColor.Black);
            Search(_regexDate, ConsoleColor.Red, ConsoleColor.Black);
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
            Search(_regexUrl, ConsoleColor.Blue, ConsoleColor.Black);
            Search(_regexDate, ConsoleColor.Red, ConsoleColor.Black);
        }

        private void ProcessUserInput(String userInput)
        {
            String query = userInput;
            if (query == "")
            {
                Console.Clear();
                ColorPrint();
            }

            // Words that start with the query
            string subStart = @"\b$1\w*\b";
            query = Regex.Replace(query, _regexStartsWith.ToString(), subStart);
            

            // Words that end with the query
            string subEnd = @"\b\w*$1\b";
            query = Regex.Replace(query, _regexEndsWith.ToString(), subEnd);

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


            // This regex matches all urls of form:
            //  http(s) or ftp :// address
            // High ASCII characters are also supported. Live example is "www.strøm.dk".
            // Example:
            //  http://www.feeds.reuters.com/~r/reuters/topNews/~3/ptoAzETqy3w/us-usa-neilarmstrong-idUSBRE87O0B020120825
            // Allowed characters found here: http://tools.ietf.org/html/rfc3986#appendix-A
            _regexUrl =
                new Regex(
                    @"(?i:https?|ftp)://(?i:[\w+?\.\w+])+(?i:[\w\~\!\@\#\$%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?");


            _regexStartsWith = new Regex(@"(\b\w*)\*");
            _regexEndsWith = new Regex(@"\*(\w*\b)");
        }


        private static void Main(string[] args)
        {
            var p = new Program(args[0]);
        }
    }
}