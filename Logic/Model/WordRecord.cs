using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    [Serializable]
    public class WordRecord
    {
        internal string Word { get; set; }
        internal string Translation { get; set; }
        internal string Category { get; set; }
        public WordRecord()
        {

        }
        public WordRecord(string word, string translation, string category)
        {
            Word = word;
            Translation = translation;
            Category = category;
        }
    }
}
