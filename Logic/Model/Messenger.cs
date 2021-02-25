using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishBot
{
    public class Messenger
    {
        private ITelegramBotClient botClient;
        private CommandParser parser;
        private WordRecord tempWord;

        public Messenger(ITelegramBotClient botClient, CommandParser commandParser)
        {
            this.botClient = botClient;
            //parser = new CommandParser();           
            parser = commandParser;

            //RegisterCommands();

        }

        //private void RegisterCommands()
        //{
        //    parser.AddCommand(new SayHiCommand());
        //    parser.AddCommand(new AskMeCommand());
        //    parser.AddCommand(new TrainingButtonCommand(botClient,));
        //    parser.AddCommand(new AddWordCommand());
        //}

        public async Task MakeAnswer(Conversation chat)
        {
            var lastmessage = chat.GetLastMessage();
            // Если есть активный диалог, передаем управление в метод AddWordDialog

            //if (chat.GetDialogState() != "Inactive")
            //{

            //    await AddWordDialog(chat, lastmessage);
            //}
            //else
            //{
            //    if (parser.IsMessageCommand(lastmessage))
            //    {

            //        //если сообщение команда, проверяем что команда /addword, если да то начинаем новый диалог в чате.
            //        if (lastmessage == "/addword")
            //        {
            //            tempWord = new WordRecord();
            //            //chat.VocabularyReadFromFile();
            //            chat.ChangeDialogState("ActiveAddWord");
            //            await ExecCommand(chat, lastmessage);
            //            await AddWordDialog(chat, lastmessage);
            //        }
            //        else
            //            await ExecCommand(chat, lastmessage);

            //    }
            //    else
            //    {
            //        //если сообщение не команда, надо проверить состояние есть ли активный диалог?
            //        //если диалог активен передать управление в метод AddWordDialog()
            //        if (chat.GetDialogState() != "Inactive")
            //        {
            //            await AddWordDialog(chat, lastmessage);
            //        }
            //        var text = CreateTextMessage();

            //        await SendText(chat, text);
            //    }
            //}
            if (parser.IsMessageCommand(lastmessage))
            {

                //если сообщение команда, проверяем что команда /addword или /stop, если да то выполняеми логику иначе выполняем команду
                switch (lastmessage)
                {
                    case "/addword":
                        tempWord = new WordRecord();
                        //chat.VocabularyReadFromFile();
                        chat.ChangeDialogState("AddWordDialog");
                        await ExecCommand(chat, lastmessage);
                        await AddWordDialog(chat, lastmessage);
                        break;
                    case "/stop":
                        chat.StopDialog();
                        await ExecCommand(chat, lastmessage);                        
                        break;
                    default:
                        await ExecCommand(chat, lastmessage);
                        break;
                }               
                    

            }
            else
            {
                //если сообщение не команда, надо проверить состояние есть ли активный диалог?
                //если диалог активен передать управление в соответствующий метод
                switch (chat.GetDialogState())
                {
                    case "AddWordDialog":
                        await AddWordDialog(chat, lastmessage);
                        break;
                    case "TrainingDialog":
                        //while(chat.GetDialogState()!="Inactive" || chat.trainingVocabulary.Count>0)
                          await TrainingDialog(chat, lastmessage);
                        break;
                    default:
                        var text = CreateTextMessage();
                        await SendText(chat, text);
                        break;
                }

                //if (chat.GetDialogState() != "Inactive")
                //{
                //    await AddWordDialog(chat, lastmessage);
                //}
                
            }
            
        }
        /// <summary>
        /// Метод отрабатывает диалог по вводу данных для объекта WordRecord
        /// </summary>
        /// <param name="chat">текущий чат с пользователе</param>
        /// <param name="message">сообщение  от пользователя</param>
        /// <returns></returns>
        public async Task AddWordDialog(Conversation chat, string message)
        {
            switch (chat.GetDialogInnerState())
            {
                case "Inactive":
                    chat.ChangeDialogState("EnglishWord");
                    await SendText(chat, "Введите слово на английском:");                    
                    break;
                case "EnglishWord":
                    tempWord.Word = message;
                    chat.ChangeDialogState("Translation");
                    await SendText(chat, "Введите перевод слова:");                    
                    break;
                case "Translation":
                    tempWord.Translation = message;
                    chat.ChangeDialogState("Category");
                    await SendText(chat, "Введите категорию(тематику) слова:");                    
                    break;
                case "Category":
                    tempWord.Category = message;
                    chat.VocabularyAddRecord(tempWord);
                    chat.StopDialog();
                    chat.VocabularySaveToFile();
                    await SendText(chat, $"В словарь добавлена запись:\n{tempWord.Word}\nПеревод: {tempWord.Translation}" +
                        $"\nТематика: {tempWord.Category}");
                    await SendText(chat, "Содержимое словаря:");
                    
                    chat.VocabularyReadFromFile();
                    if (chat.tempVocabulary != null)
                    {
                        foreach (var item in chat.tempVocabulary)
                        {
                            await SendText(chat, CreateVocabularyRecordString(item));
                        }
                    }                                      
                    await SendText(chat, CreateTextMessage());
                    break;
                
            }
           
        }

        //public async Task SetTraininDialog(Conversation chat, string message)
        //{
        //    switch (chat.GetDialogState())
        //    {
        //        case "Active":
        //            chat.ChangeDialogState("EnglishWord");
        //            await SendText(chat, "Введите слово на английском:");
        //            break;
        //        case "EnglishWord":
        //            tempWord.Word = message;
        //            chat.ChangeDialogState("Translation");
        //            await SendText(chat, "Введите перевод слова:");
        //            break;
        //        case "Translation":
        //            tempWord.Translation = message;
        //            chat.ChangeDialogState("Category");
        //            await SendText(chat, "Введите категорию(тематику) слова:");
        //            break;
        //        case "Category":
        //            tempWord.Category = message;
        //            chat.VocabularyAddRecord(tempWord);
        //            chat.StopDialog();
        //            chat.VocabularySaveToFile();
        //            await SendText(chat, $"В словарь добавлена запись:\n{tempWord.Word}\nПеревод: {tempWord.Translation}" +
        //                $"\nТематика: {tempWord.Category}");
        //            await SendText(chat, "Содержимое словаря:");

        //            chat.VocabularyReadFromFile();
        //            if (chat.tempVocabulary != null)
        //            {
        //                foreach (var item in chat.tempVocabulary)
        //                {
        //                    await SendText(chat, CreateVocabularyRecordString(item));
        //                }
        //            }



        //            await SendText(chat, CreateTextMessage());
        //            break;

        //    }

        //}

        /// <summary>
        /// Метод реализует диалог тренировки
        /// </summary>
        /// <param name="chat">текущий чат с пользователем</param>
        /// <param name="message"> сообщение от пользователя </param>
        /// <returns></returns>
        public async Task TrainingDialog(Conversation chat, string message)
        {
            TrainingConfig options;// = chat.GetTrainingOptions();
            switch (chat.GetDialogInnerState())
            {
                //начало тренировки
                case "Inactive":
                    options = new TrainingConfig(true, false, true, ""); // TODO сделать метод ввода параметров тренировки
                    //Задаем параметры тренировки
                    chat.SetTrainingOptions(options);
                    //Читаем первую запись из тренировочного словаря, пишем ее в темп и удаляем из словаря
                    chat.SetTrainingWord(chat.trainingVocabulary[0]);
                    chat.trainingVocabulary.RemoveAt(0);

                    if (options.IsRuToEN())
                    {
                        chat.ChangeDialogState("TrainingRu");
                        var text = "Переведите на английский:\n" + chat.GetTrainingWord().Translation;
                        await SendText(chat, text);
                        chat.TrainingGoNext();
                    }
                    else
                    {
                        var text = "Переведите на русский:\n" + chat.GetTrainingWord().Word;
                        await SendText(chat, text);
                        chat.ChangeDialogState("TrainingEn");
                        chat.TrainingGoNext();
                    } 
                    //chat.ChangeDialogState("");
                    break;
                case "TrainingRu":
                    if (message.ToLower() == chat.GetTrainingWord().Word.ToLower())
                    {
                        await SendText(chat, "Верно!");
                    }
                    else
                    {
                        var text = "Неверно - правильный ответ: " + chat.GetTrainingWord().Word;
                        await SendText(chat, text);
                    }
                    chat.ChangeDialogState("Inactive");
                    break;
                case "TrainingEN":
                    if (message.ToLower() == chat.GetTrainingWord().Translation.ToLower())
                    {
                        await SendText(chat, "Верно!");
                    }
                    else
                    {
                        var text = "Неверно - правильный ответ: " + chat.GetTrainingWord().Translation;
                        await SendText(chat, text);
                    }
                    chat.ChangeDialogState("Inactive");
                    break;               

            }

        }


        /// <summary>
        /// Выполняет команду
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task ExecCommand(Conversation chat, string command)
        {
            if (parser.IsTextCommand(command))
            {
                var text = parser.GetMessageText(command);

                await SendText(chat, text);
            }

            if (parser.IsButtonCommand(command))
            {
                var keys = parser.GetKeyBoard(command);
                var text = parser.GetInformationalMeggase(command);
                parser.AddCallback(command, chat.GetId());

                await SendTextWithKeyBoard(chat, text, keys);
            }
        }
        /// <summary>
        /// Отправляет приветственное сообщение
        /// </summary>
        /// <param name="chat"></param>
        public async void  SendStartMessage(Conversation chat)
        {
            await SendText(chat, CreateTextMessage());
        }

        private string CreateTextMessage()
        {
            var text = "Команды бота:\n" +
                       "/sayhi  - приветствие\n" +
                       "/askme  - вопрос от бота\n" +
                       "/training - Тренировка\n" +
                       "/stop - остановить тренировку\n"+
                       "/addword - добавить слово в словарь";                        

            return text;
        }
        /// <summary>
        /// формирует строку из WordRecord
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string CreateVocabularyRecordString(WordRecord data)
        {
            var text = $"{data.Word}\nПеревод: {data.Translation}" +
                        $"\nТематика: {data.Category}";

            return text;
        }
        /// <summary>
        /// Посылает текстовое сообщение в чат
        /// </summary>
        /// <param name="chat">Текущий чат с пользователем</param>
        /// <param name="text">Текст сообщения</param>
        /// <returns></returns>
        private async Task SendText(Conversation chat, string text)
        {

            await botClient.SendTextMessageAsync(
                  chatId: chat.GetId(),
                  text: text
                );
        }

        private async Task SendTextWithKeyBoard(Conversation chat, string text, InlineKeyboardMarkup keyboard)
        {
            await botClient.SendTextMessageAsync(
                  chatId: chat.GetId(),
                  text: text,
                  replyMarkup: keyboard
                );
        }
    }


}
