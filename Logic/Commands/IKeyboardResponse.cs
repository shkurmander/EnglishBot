using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishBot
{
    interface IKeyboardResponse
    {
        InlineKeyboardMarkup ReturnKeyBoard();

        void AddCallBack(long chatid);

        string InformationalMessage();

    }
}