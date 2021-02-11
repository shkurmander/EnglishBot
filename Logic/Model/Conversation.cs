using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace EnglishBot
{
    public class Conversation
    {
        private Chat telegramChat;

        private List<Message> telegramMessages;
        private AddwordDialog addwordDialog;
        
        public List<WordRecord> vocabulary; //TODO сменить модификатор доступа
        public List<WordRecord> tempVocabulary;


        public Conversation(Chat chat)
        {
            telegramChat = chat;
            telegramMessages = new List<Message>();
            addwordDialog = new AddwordDialog();
            vocabulary = new List<WordRecord>();            
        }

        public void AddMessage(Message message)
        {
            telegramMessages.Add(message);
        }

        public long  GetId() => telegramChat.Id;

        public string GetLastMessage() => telegramMessages[telegramMessages.Count-1].Text;

        public List<string> GetTextMessages()
        {
            var textMessages = new List<string>();

            foreach (var message in telegramMessages)
            {
                if (message.Text != null)
                {
                    textMessages.Add(message.Text);
                }
            }

            return textMessages;
        }
        /// <summary>
        /// метод создает новый диалог
        /// </summary>
        public void StartDialog() => addwordDialog.SetState("Active");
        /// <summary>
        /// метод удаляет диалог
        /// </summary>     
        public void StopDialog() => addwordDialog.SetState("Inactive");

        /// <summary>
        /// метод задает состояние диалога
        /// </summary>
        /// <param name="state"></param>
        public void ChangeDialogState(string state) => addwordDialog.SetState(state);
        /// <summary>
        /// метод отдает состояние диалога
        /// </summary>
        /// <returns></returns>
        public string GetDialogState() => addwordDialog.GetState();
        /// <summary>
        /// метод добавляет новую запись в словарь
        /// </summary>
        /// <param name="record"></param>
        public void VocabularyAddRecord(WordRecord record) => vocabulary.Add(record);
        /// <summary>
        /// метод пишет словарь в бинарный файл
        /// </summary>
        public void VocabularySaveToFile()
        {            
            var path = "user" + telegramChat.Id.ToString() + ".dat.";            
            
            DataSaver<List<WordRecord>>.BinaryDataSave(vocabulary, path);
            
        }
        /// <summary>
        /// Метод читает словарь из файла
        /// </summary>
        public void VocabularyReadFromFile()
        {
            var path = "user" + telegramChat.Id.ToString() + ".dat.";
            var data = DataSaver<List<WordRecord>>.BinaryDataRead(path);
            if (System.IO.File.Exists(path))
            {
                if (data != null)
                    tempVocabulary = data;
                else
                    Console.WriteLine("Не удалось считать словарь!");
            }
            else
                Console.WriteLine("Словарь еще не создан!");

        }

    }
}
