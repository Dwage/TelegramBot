using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace telegramBot.TelegramBotSource
{
    public class TelegramBot
    {
        private TelegramBotClient Client { get; }
        private MessageListener message;
        public TelegramBot(string token) => Client = new TelegramBotClient(token);

        public void Start()
        {
            Client.StartReceiving();
            message = new MessageListener(Client);
            message.Subscribe();
        }

        public void Stop()
        {
            Client.StopReceiving();
            message.UnSubscribe();
        }
    }
}
