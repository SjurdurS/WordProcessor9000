﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WordProcessor9000
{
    class Program
    {

        public static String reader;


        /// <summary>
        /// Writes a string to the console
        /// </summary>
        /// <param name="str"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="foregroundColor"></param>
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

        static void Main(string[] args)
        {
            String file = args[0];
            reader = TextFileReader.ReadFile(file);
            Console.WriteLine(reader);

            // This regex matches the format of the dates in the given example text file:
            // "DAY, DATE MONTH YEAR" 
            // Where:
            //  DAY is the three letter abbreviation of the day
            //  DATE is one or two digit date
            //  MONTH is the abbreviation of the Month
            //  YEAR is either 1xxx or 2xxx.
            Regex dateRegex = new Regex(@"(?i:mon|tue|wed|thu|fri|sat|sun),\s\d\d?\s(?i:jan|feb|mar|apr|may|june|july|aug|sept|oct|nov|dev)\s(?:[12]\d{3})");

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


            HighligthedConsoleWrite("This is a test of the highlighted word method", ConsoleColor.Red, ConsoleColor.Green);
            HighligthedConsoleWrite("This is another test of the highlighted word method", ConsoleColor.Green, ConsoleColor.Red);

            ColouredConsoleWrite("This is a test of the coloured word method", ConsoleColor.Cyan);
        }
    }
}
