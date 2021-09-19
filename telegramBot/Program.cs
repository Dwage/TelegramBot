using System;
using telegramBot.TelegramBotSource;
using telegramBot.TelegramBotSource.Logger;

namespace telegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            TelegramBot telegramBot = new TelegramBot("2042854421:AAFpDqkTxhQo6XFN7e_XgMB20e9kU4C7Xww");
            telegramBot.Start();
            Console.ReadKey();
            telegramBot.Stop();
        }
    }
}
