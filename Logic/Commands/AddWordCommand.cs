using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public class AddWordCommand : AbstractCommand, IChatTextResponse
    {
        private Conversation chat;
        public AddWordCommand(Conversation conversation)
        {
            CommandText = "/addword";
            chat = conversation;
        }
      
        public string ReturnText()
        {
            
            return "Добавление нового слова в словарь...";
        }

       

    }
}
