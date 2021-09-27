using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using telegramBot.Source.Parser;
using telegramBot.Source.Parser.ConcreteParser;

namespace telegramBot.Source.Command.Commands
{
    class ParserCommand : Command
    {
        public override string[] Names { get; set; } = { "parse" };
        private ParserWorker<string[]> parser;
        private Message message;
        private TelegramBotClient client;
        private string url;
        private string tagToParseBy;
        public async override void Execute(Message message, TelegramBotClient client)
        {
            this.message = message;
            this.client = client;

            ProcessMessage(message);

            if (CheckURLValid(url))
            {
                parser = new ParserWorker<string[]>(new TextParser());
                parser.Settings = new HtmlParserSettings(url, tagToParseBy);
                parser.OnNewData += Print;
                parser.Start();
            }
            else
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Invalid url");
            }
        }
        public bool CheckURLValid(string url)
            => Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
               && uriResult.Scheme == Uri.UriSchemeHttps;

        private void ProcessMessage(Message message)
        {
            string[] messageWords = message.Text.Split(' ');
            url = string.Empty;
            tagToParseBy = string.Empty;
            if (messageWords.Length >= 2)
                url = messageWords[1];
            if (messageWords.Length >= 3)
            {
                for (int i = 2; i < messageWords.Length; i++)
                    tagToParseBy += messageWords[i];
            }
        }

        private void Print(object arg1, string[] args)
        {
            args.Distinct().ToArray();
            foreach(string arg in args)
                client.SendTextMessageAsync(message.Chat.Id, arg);
            parser.Abort();
        }
    }
}
