using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public class AddWordCommand : AbstractCommand, IChatTextResponse
    {
        public AddWordCommand()
        {
            CommandText = "/addword";
        }
      
        public string ReturnText()
        {
            return "Добавление нового слова в словарь...";
        }

    }
}
