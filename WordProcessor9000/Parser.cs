using System;
using System.IO;
using WordProcessor9000.Properties;

namespace WordProcessor9000
{
    internal class Parser
    {
        private readonly CommandWords _commands;

        public Parser()
        {
            _commands = new CommandWords();
        }

        public Command GetCommand()
        {
            String inputLine = null;

            Console.Write(Resources.InputMessage);

            try
            {
                inputLine = Console.ReadLine();
            }
            catch (IOException ie)
            {
                Console.WriteLine("Error occured while reading input: " + ie);
            }

            if (_commands.IsCommand(inputLine))
                return new Command(inputLine);
            return new Command(null);
        }
    }
}