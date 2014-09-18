using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor9000
{
    class Command
    {
        private String _commandWord;

        public Command(String commandWord)
        {
            this._commandWord = commandWord;
        }

        public String GetCommandWord()
        {
            return _commandWord;
        }

        public Boolean IsUnknown()
        {
            return (_commandWord == null);
        }
    }
}
