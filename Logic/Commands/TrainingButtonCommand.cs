using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishBot
{
    public class TrainingButtonCommand : AbstractCommand, IKeyboardResponse
    {
        ITelegramBotClient botClient;
        Messenger messenger;
        Conversation chat;

        public TrainingButtonCommand(ITelegramBotClient botClient, Messenger msngr, Conversation chat)
        {
            this.botClient = botClient;
            messenger = msngr;
            this.chat = chat;
            CommandText = "/training";
        }

        public void AddCallBack(long ChatId)
        {
            this.botClient.OnCallbackQuery -= Bot_Callback;
            this.botClient.OnCallbackQuery += Bot_Callback;
        }

        private async void Bot_Callback(object sender, CallbackQueryEventArgs e)
        {


            //var text = "";


            switch (e.CallbackQuery.Data)
            {

                case "Начать тренировку":
                    chat.StartDialog();
                    await messenger.TrainingDialog(chat, chat.GetLastMessage());
                    break;
                case "Запланировать тренировку":
            
                    break;
                default:
                    break;
            }

            await botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, text);
            await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
        }

        public InlineKeyboardMarkup ReturnKeyBoard()
        {
            var buttonList = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton
                {
                    Text = "Начать тренировку",
                    CallbackData = "Начать тренировку"
                },

                new InlineKeyboardButton
                {
                    Text = "Запланировать тренировку",
                    CallbackData = "Запланировать тренировку"
                }
            };

            var keyboard = new InlineKeyboardMarkup(buttonList);

            return keyboard;
        }

        public string InformationalMessage()
        {
            return "Что вы хотите сделать?";
        }
    }
}
