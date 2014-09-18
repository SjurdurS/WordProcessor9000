using System;
using System.IO;
using WordProcessor9000.Properties;

namespace WordProcessor9000
{
    internal class Parser
    {
        public Parser()
        {
        }

        public String GetUserInput()
        {
            String inputLine = "";

            try
            {
                Console.Write("> ");
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