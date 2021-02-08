using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishBot
{
    public class CommandParser
    {
        private List<IChatResponse> Command;

        public CommandParser()
        {
            Command = new List<IChatResponse>();
        }

        public void AddCommand(IChatResponse chatCommand)
        {
            Command.Add(chatCommand);
        }

        public bool IsMessageCommand(string message)
        {
            return Command.Exists(x => x.CheckMessage(message));
        }

        public bool IsTextCommand(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            return command is IChatTextResponse;
        }

        public bool IsButtonCommand(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            return command is IKeyboardResponse;
        }

        public string GetMessageText(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IChatTextResponse;

            return command.ReturnText();
        }

        public string GetInformationalMeggase(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyboardResponse;

            return command.InformationalMessage();
        }

        public InlineKeyboardMarkup GetKeyBoard(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyboardResponse;

            return command.ReturnKeyBoard();
        }

        public void AddCallback(string message, long chatId)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyboardResponse;
            command.AddCallBack(chatId);
        }
    }
}
