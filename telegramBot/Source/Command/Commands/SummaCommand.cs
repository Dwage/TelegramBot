using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace telegramBot.Source.Command.Commands
{
    public class SummaCommand : Command
    {
        CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture; //toDo.
        public override string[] Names { get; set; } = new string[] { "sum", "сумма", "summa" };

        public override async void Execute(Message message, TelegramBotClient client)
        {
            List<double> numbers = GainNumbers(message);
            await client.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел: {numbers.Sum()}");
        }

        private List<double> GainNumbers(Message message)
        {
            string[] messageElements = message.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            List<double> numbers = new List<double>();
            double number;
            for (int i = 0; i < messageElements.Length; i++)
            {
                string element = messageElements[i];

                if (element.Contains('.'))
                    element = element.Replace('.', ',');
                if (double.TryParse(element, out number))
                    numbers.Add(number);
            }
            return numbers;
        }
    }
}
