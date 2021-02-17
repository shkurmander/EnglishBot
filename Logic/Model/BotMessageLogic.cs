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
        private CommandParser parser;
        private ITelegramBotClient botClient;

        private Dictionary<long, Conversation> chatList;
              

        public BotMessageLogic(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
            parser = new CommandParser();
            RegisterCommands();
            messenger = new Messenger(botClient, parser);            
            chatList = new Dictionary<long, Conversation>();            

        }
        private void RegisterCommands()
        {
            parser.AddCommand(new SayHiCommand());
            parser.AddCommand(new AskMeCommand());
            parser.AddCommand(new AddWordCommand());
            //parser.AddCommand(new TrainingButtonCommand(this.botClient,this.messenger));            
        }

        public async Task Response(MessageEventArgs e)
        {
            var id = e.Message.Chat.Id;
            if (!chatList.ContainsKey(id))
            {
                var newchat = new Conversation(e.Message.Chat);                
                chatList.Add(id, newchat);
                parser.AddCommand(new TrainingButtonCommand(this.botClient, this.messenger, newchat));
            }

            var chat = chatList[id];
            //if (chat.GetTextMessages().Count == 0)
            //{
            //    messenger.SendStartMessage(chat);
            //}
            chat.AddMessage(e.Message);
            parser.AddCommand(new TrainingButtonCommand(this.botClient, this.messenger, chat));

            await SendMessage(chat);
        }


        private async Task SendMessage(Conversation chat)
        {
            await messenger.MakeAnswer(chat);

        }

    


    }
}
