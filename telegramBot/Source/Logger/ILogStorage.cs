using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telegramBot.TelegramBotSource.Logger
{
    interface ILogStorage
    {
        void Create();
        void Write();
    }
}
