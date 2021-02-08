using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public interface IChatCommand
    {
        bool CheckMessage(string message);
    }
}
