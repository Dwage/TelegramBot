using Telegram.Bot;
using telegramBot.Source;

namespace telegramBot.TelegramBotSource
{
    public class MessageListener
    {
        private TelegramBotClient Client { get; }
        private MessageHandler messageHandler;
        public MessageListener(TelegramBotClient client)
        {
            Client = client;
            messageHandler = new MessageHandler(Client);
        }
        public void Subscribe() => Client.OnMessage += messageHandler.OnMessageHandler;
        public void UnSubscribe() => Client.OnMessage -= messageHandler.OnMessageHandler;
    }
}
