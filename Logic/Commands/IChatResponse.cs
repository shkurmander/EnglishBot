using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public interface IChatResponse
    {
        bool CheckMessage(string message);
    }
}
