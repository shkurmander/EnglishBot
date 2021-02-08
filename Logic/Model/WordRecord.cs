using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public class WordRecord
    {
        private string Word { get; }
        private string Translation { get; }
        private string Category { get; }

        public WordRecord(string word, string translation, string category)
        {
            Word = word;
            Translation = translation;
            Category = category;
        }
    }
}
