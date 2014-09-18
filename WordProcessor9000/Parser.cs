using System;
using System.IO;
using WordProcessor9000.Properties;

namespace WordProcessor9000
{
    internal class Parser
    {
        private readonly Commands _commands;

        public Parser()
        {
            _commands = new Commands();
        }

        public Command GetCommand()
        {
            String inputLine = null;

            try
            {
                Console.Write("> ");
                inputLine = Console.ReadLine();
            }
            catch (IOException ie)
            {
                Console.WriteLine("Error occured while reading input: " + ie);
            }

            return new Command(inputLine);
        }
    }
}