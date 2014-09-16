using System;

namespace WordProcessor9000
{
    internal class CommandWords
    {
        private static readonly String[] ValidCommands =
        {
            "help",
            "quit"
            
        };

        public Boolean IsCommand(String aString)
        {
            for (int i = 0; i < ValidCommands.Length; i++)
            {
                if (ValidCommands[i].Equals(aString, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }


    }
}