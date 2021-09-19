using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace telegramBot.Source.Command.Commands
{
    public class GetMyIdCommand : Command
    {
        public override string[] Names { get; set; } = new string[] { "getmyid", "get_my_id", "мой айди" };

        public override async void Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, $"Ваш телеграм ID: {message.From.Id}");
        }
    }
}
