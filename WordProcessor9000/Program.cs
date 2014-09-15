using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            


            HighligthedConsoleWrite("This is a test of the highlighted word method", ConsoleColor.Red, ConsoleColor.Green);
            HighligthedConsoleWrite("This is another test of the highlighted word method", ConsoleColor.Green, ConsoleColor.Red);

            ColouredConsoleWrite("This is a test of the coloured word method", ConsoleColor.Cyan);
        }
    }
}
