using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public class SayHiCommand : AbstractCommand, IChatTextResponse
    {
        public SayHiCommand()
        {
            CommandText = "/sayhi";
        }

        public string ReturnText()
        {
            return "Здарова, отец!";
        }

    }
}
