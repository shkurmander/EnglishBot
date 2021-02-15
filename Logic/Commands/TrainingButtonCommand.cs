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

        public TrainingButtonCommand(ITelegramBotClient botClient, Messenger msngr)
        {
            this.botClient = botClient;
            messenger = msngr;
            CommandText = "/training";
        }

        public void AddCallBack(long ChatId)
        {
            this.botClient.OnCallbackQuery -= Bot_Callback;
            this.botClient.OnCallbackQuery += Bot_Callback;
        }

        private async void Bot_Callback(object sender, CallbackQueryEventArgs e)
        {
            var text = "";

            
            switch (e.CallbackQuery.Data)
            {
                
                case "Начать тренировку":
                    text = @"На белой лестнице встретился один,
                                На чёрной лестнице повстречал другого…
                                Белое, чёрное, красное, зелёное –
                                Вот и жизнь проходит, словно сон.
                                Припев:
                                Сон купца, сон купца –
                                Словно старая мельница.
                                Сон купца, сон купца
                                Никуда не денется, ца-ца.";
                    break;
                case "Запланировать тренировку":
                    text = @"Больше никогда,
                            Не будет ничего
                            Нихрена!
                            Не произойдёт!
                            Никогда!!!
                            Ни тогда ни сейчас ни потом,
                            Ничего всё фигня и отстой...";
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
