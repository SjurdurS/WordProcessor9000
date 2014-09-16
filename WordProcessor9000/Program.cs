using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordProcessor9000
{
    class Program
    {


        public static String Reader;
        private Parser parser;

        private void Setup(String filepath)
        {
            parser = new Parser();
            Reader = TextFileReader.ReadFile(filepath);
        }

        private void PrintWelcome()
        {
            Console.WriteLine("Welcome to WordProcessor9000");
            Console.WriteLine("This software allows the user to search for and highlight words a given text file.");
            Console.WriteLine("Type 'help' if you need help.");
        }

        public void Start()
        {
            PrintWelcome();
            Boolean finished = false;
            while (!finished)
            {
                Command command = parser.GetCommand();
                finished = processCommand(command);
            }
        }

        private void printHelp()
        {
            Console.WriteLine("To search for a word type in the word separated by a white space and hit enter key.");
            Console.WriteLine("To exit the program type 'quit' and press any key");

        }

        private Boolean processCommand(Command command)
        {
            Boolean wantToQuit = false;

            if (command.IsUnknown())
            {
                Console.WriteLine("Invalid command entered.");
                return false;
            }

            String commandWord = command.getCommandWord();
            if (commandWord.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                printHelp();
            }
            else if (commandWord.Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                wantToQuit = true;
            }

            return wantToQuit;
        }

        /// <summary>
        /// Writes a string to the console with the specified background / foreground colors.
        /// </summary>
        /// <param name="str">The string to color</param>
        /// <param name="backgroundColor">The background color</param>
        /// <param name="foregroundColor">The font color</param>
        private static void HighligthedConsoleWrite(String str, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor; 
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(str);
            Console.BackgroundColor = originalBackgroundColor;
            Console.ForegroundColor = originalForegroundColor;
        }

        private static void ColouredConsoleWrite(String str, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ForegroundColor = originalColor;
        }

        private void regexes()
        {
            // This regex matches the format of the dates in the given example text file:
            // "DDD, dd MMM(M) YYYY" 
            // Where:
            //  DDD is the three letter abbreviation of the days name
            //  dd is one or two digit date
            //  MMM(M) is the abbreviation of the Month
            //  YYYY is either 1xxx or 2xxx.
            Regex dateRegex = new Regex(@"(?i:mon|tue|wed|thu|fri|sat|sun),\s\d\d?\s(?i:jan|feb|mar|apr|may|june|july|aug|sept|oct|nov|dec)\s(?:[12]\d{3})");


            // This regex matches all urls of forms:
            //  http(s) or ftp :// address
            // OR
            //  www. address
            // High ASCII characters are also supported. Live example is "www.strøm.dk".
            // Examples:
            //  http://www.feeds.reuters.com/~r/reuters/topNews/~3/ptoAzETqy3w/us-usa-neilarmstrong-idUSBRE87O0B020120825
            //  www.feeds.reuters.com/
            // Allowed characters found here: http://tools.ietf.org/html/rfc3986#appendix-A
            Regex urlRegex = new Regex(@"(?i:(?i:https?|ftp)://|[^w]www\.)(?i:[\w+?\.\w+])+(?i:[\w\~\!\@\#\$%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?");
        }




        static void Main(string[] args)
        {
            Program p = new Program();
            p.Setup(args[0]);
            p.Start();

        }
    }
}
