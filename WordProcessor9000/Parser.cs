using System;
using System.IO;
using WordProcessor9000.Properties;

namespace WordProcessor9000
{

    internal class Parser
    {
        /// <summary>
        /// Get the user input from the Console.
        /// </summary>
        /// <returns>Returns the input by from the Console.</returns>
        public String GetUserInput()
        {
            String inputLine = "";

            try
            {
                Console.Write("> "); // InputMessage
                inputLine = Console.ReadLine();
            }
            catch (IOException ie)
            {
                Console.WriteLine("Error occured while reading input: " + ie);
            }

            return inputLine;
        }
    }
}