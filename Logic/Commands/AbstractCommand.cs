using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public abstract class AbstractCommand : IChatResponse
    {
        public string CommandText;

        public bool CheckMessage(string message)
        {
            return CommandText == message;
        }
    }
}
