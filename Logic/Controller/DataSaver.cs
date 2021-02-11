using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

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
        public static void BinaryDataSave(T data, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Console.WriteLine($"Запись в файл \"{path}\"\n");

            try
            {
                //Сериализуем 
                using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            
        }
    }
}
