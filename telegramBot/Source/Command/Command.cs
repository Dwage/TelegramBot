using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace telegramBot.Source.Command
{
    public abstract class Command
    {
        public abstract string[] Names { get; set; }
        public abstract void Execute(Message message, TelegramBotClient client);

        public bool Equals(string message)
        {
            foreach (var commandName in Names)
            {
                if (commandName.Equals(message))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
