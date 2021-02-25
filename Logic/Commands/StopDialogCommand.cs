using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishBot
{
    public class StopDialogCommand : AbstractCommand, IChatTextResponse
    {
        
        public StopDialogCommand()
        {
            CommandText = "/stop";
        }

        public string ReturnText()
        {

            return "Тренировка остановлена...";
        }



    }
}
