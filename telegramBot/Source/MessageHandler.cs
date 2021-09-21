using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using telegramBot.Source.Command.Commands;
using telegramBot.Source.Logger;

namespace telegramBot.Source
{
    public class MessageHandler
    {
        private MessageStorage messageStorage = new MessageStorage();
        private List<Command.Command> commands = new List<Command.Command>();
        private TelegramBotClient client;

        public MessageHandler(TelegramBotClient client)
        {
            this.client = client;
            commands.Add(new SummaCommand());
        }

        internal async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            Message message = e.Message;
            if (message != null && message.Type == MessageType.Text)
            {
                Console.WriteLine(message.Text);
                await messageStorage.Save(message.Text);

                await Task.Run(() => ProcessMessageText(message));
            }
        }
        public void ProcessMessageText(Message message)
        {
            string firstWordOfMessage = message.Text.Split(' ')[0];

            string specialSymbol = firstWordOfMessage.Substring(0, 1);
            firstWordOfMessage = firstWordOfMessage.Remove(0, 1);

            switch (specialSymbol)
            {
                case "/":
                    foreach (var command in commands)
                    {
                        if (command.Equals(firstWordOfMessage))
                        {
                            command.Execute(message, client);
                        }
                    }
                    break;
                case "*":
                    break;
                default:
                    break;
            }
        }
    }
}
