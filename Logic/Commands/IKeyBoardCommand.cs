using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishBot
{
    interface IKeyBoardCommand
    {
        InlineKeyboardMarkup ReturnKeyBoard();

        void AddCallBack(long chatid);

        string InformationalMessage();

    }
}