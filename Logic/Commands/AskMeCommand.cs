﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot

{
    public class AskMeCommand : AbstractCommand, IChatTextResponse
    {
        public AskMeCommand()
        {
            CommandText = "/askme";
        }

        public string ReturnText()
        {
            return "Ю ноу, блин?";
        }

    }
}