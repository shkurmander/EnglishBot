using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public class AddWordCommand : AbstractCommand, IChatTextResponse, IChatTextDialog
    {
        public AddWordCommand()
        {
            CommandText = "/addword";
        }

        public string AddData(string text)
        {
            return text;
        }

        public string ReturnText()
        {
            return "Ю ноу, блин?";
        }

    }
}
