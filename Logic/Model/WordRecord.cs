using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EnglishBot
{
    
    [Serializable]
    
    
    public class WordRecord : IEquatable<WordRecord> 
    {
        internal string Word { get; set; }
        internal string Translation { get; set; }
        internal string Category { get; set; }       
        public WordRecord(string word, string translation, string category)
        {
            Word = word;
            Translation = translation;
            Category = category;
        }
        /// <summary>
        /// Переопределяем метод GetHashCode, возвращаем хэш для возможности сравнения объектов WordRecord
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (Word == null ? 0 : Word.GetHashCode()) ^ (Translation == null ? 0 : Translation.GetHashCode());
        }
        /// <summary>
        /// Определяет правило сравнения объекта WordRecord с другим объектом WordRecord
        /// </summary>
        /// <param name="wordrecord"></param>
        /// <returns></returns>
        public bool Equals(WordRecord wordrecord)
        {
            return Word == wordrecord.Word && Translation == wordrecord.Translation;
        }

        
    }
}
