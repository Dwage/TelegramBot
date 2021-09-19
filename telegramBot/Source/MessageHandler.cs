using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using telegramBot.Source.Command.Commands;
using telegramBot.Source.Logger;

namespace telegramBot.Source
{
    public class MessageHandler
    {
        private MessageStorage messageStorage = new MessageStorage();
        private List<Command.Command> commands = new List<Command.Command>();;
        private TelegramBotClient client;

        public MessageHandler(TelegramBotClient client)
        {
            this.client = client;
        }

        internal async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            Message message = e.Message;
            if (message != null)
            {
                Console.WriteLine(message.Text);
                await messageStorage.Save(message.Text);

                foreach (var comm in commands)
                {
                    if (comm.Equals(message.Text))
                    {
                        comm.Execute(message, client);
                    }
                }

                //if (message.Type == MessageType.Text)
                    //await Task.Run(() => ProcessMessageText(message));
                //else
                    //await Task.Run(() => ProcessMessageOther(message));
            }
        }
        /*public void ProcessMessageText(Message message)
        {
            string specialSymbol = message.Text.Substring(0, 1);
            switch (specialSymbol)
            {
                case "/":
                    Command.Process(message);
                    break;
                case "*":
                    CodeProcessing(message);
                    break;
                default:
                    break;
            }
        }*/
        public void ProcessMessageOther(Message message)
        {

        }
    }
}
