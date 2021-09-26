using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace telegramBot.Source.Command.Commands
{
    public class ArithmeticCommand : Command
    {
        public override string[] Names { get; set; } = new string[] { "arithmeticOperation" };

        public override async void Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, $"Result: {Calc(message.Text)}");
        }
        private string FormatMathExpression(string mathExpression)
        {
            mathExpression = Regex.Replace(mathExpression, @"((?<=\d)(?=\p{Sm})|(?<=\p{Sm})(?=\d))", " ");
            return Regex.Replace(mathExpression, @"\p{Sm}$", "");
        }

        private double Calc(string mathExpression)
        {
            mathExpression = FormatMathExpression(mathExpression);
            string[] parts = mathExpression.Split(' '); 

            var operands = new List<double>();
            var operations = new List<string>();
            for (var i = 0; i < parts.Length; i += 2)
            {
                operands.Add(Convert.ToDouble(parts[i]));
                if (i + 1 < parts.Length)
                    operations.Add(parts[i + 1]);
            }
            Calculate(operands, operations, "*", (a, b) => a * b, "/", (a, b) => a / b);
            Calculate(operands, operations, "+", (a, b) => a + b, "-", (a, b) => a - b);

            return operands[0]; 
        }

        private void Calculate(List<double> operands, List<string> operations,
            string currentOperation1, Func<double, double, double> function1,
            string currentOperation2, Func<double, double, double> function2)
        {
            while (true)
            {
                var i1 = operations.IndexOf(currentOperation1);
                var i2 = operations.IndexOf(currentOperation2);

                int index; 
                if (i1 >= 0 && i2 >= 0)
                    index = Math.Min(i1, i2);
                else
                    index = Math.Max(i1, i2);

                if (index < 0)
                    break;

                if (index == i1)
                    operands[index] = function1(operands[index], operands[index + 1]);
                else
                    operands[index] = function2(operands[index], operands[index + 1]);
                operations.RemoveAt(index);
                operands.RemoveAt(index + 1);
            }
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
