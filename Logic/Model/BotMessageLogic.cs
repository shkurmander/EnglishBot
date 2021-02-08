using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace EnglishBot
{
    public class BotMessageLogic
    {
        private Messenger messenger;

        private Dictionary<long, Conversation> chatList;
              

        public BotMessageLogic(ITelegramBotClient botClient)
        {            
            messenger = new Messenger(botClient);            
            chatList = new Dictionary<long, Conversation>();
            
        }
        public async Task Response(MessageEventArgs e)
        {
            var id = e.Message.Chat.Id;
            if (!chatList.ContainsKey(id))
            {
                var newchat = new Conversation(e.Message.Chat);
                chatList.Add(id, newchat);
            }

            var chat = chatList[id];
            chat.AddMessage(e.Message);

            await SendMessage(chat);
        }


        private async Task SendMessage(Conversation chat)
        {
            await messenger.MakeAnswer(chat);

        }

       
    }
}
