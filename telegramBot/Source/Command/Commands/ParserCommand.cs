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
        private ParserWorker<string[]> parserWorker;
        private Message message;
        private TelegramBotClient client;
        public async override void Execute(Message message, TelegramBotClient client)
        {
            this.message = message;
            this.client = client;

            string url = string.Empty;
            string tagToParseBy = string.Empty;
            ProcessMessage(message, ref url, ref tagToParseBy);

            if (CheckURLValid(url) && tagToParseBy != string.Empty)
            {
                parserWorker = new ParserWorker<string[]>(new ByTagHtmlParser());
                parserWorker.Settings = new HtmlParserSettings(url, tagToParseBy);
                parserWorker.OnNewData += Print;
                parserWorker.Start();
            }
            else
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Invalid url or the tag is not specified");
            }
        }
        public bool CheckURLValid(string url)
            => Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
               && uriResult.Scheme == Uri.UriSchemeHttps;

        private void ProcessMessage(Message message, ref string url, ref string tagToParseBy)
        {
            string[] messageWords = message.Text.Split(' ');

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
            if (args.Length != 0)
            {
                args.Distinct().ToArray();
                foreach (string arg in args)
                    client.SendTextMessageAsync(message.Chat.Id, arg);
            }
            else
                client.SendTextMessageAsync(message.Chat.Id, "Nothing was found");

            parserWorker.Abort();
        }
    }
}
