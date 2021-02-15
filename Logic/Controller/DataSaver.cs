using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Linq;

namespace EnglishBot
{
    public static class  DataSaver<T> 
    {       
        public static T BinaryDataRead( string path)
        {
            
            BinaryFormatter formatter = new BinaryFormatter();
            Console.WriteLine($"Читаем данные из файла \"{path}\"\n");
            //Десериализуем данные из файла и пишем их в массив объектов Students и попутно выводим
            try
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var data = (T)formatter.Deserialize(fs);
                    return data;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ошибка - файл данных пользоателя не существует!");
                return default;
            }

        }
        public static void BinaryDataSave(List<WordRecord> data, string path) 
        {
            BinaryFormatter formatter = new BinaryFormatter();

            var oldData = DataSaver<List<WordRecord>>.BinaryDataRead(path);

            List<WordRecord> newData;

            if (oldData != null)
            {
                newData = data.Union(oldData).ToList();
            }
            else
            {
                newData = data;
            }
            

            Console.WriteLine($"Запись в файл \"{path}\"\n");

            try
            {
                //Сериализуем 
                using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                   
                    formatter.Serialize(fs, newData);
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            
        }
    }
}
