using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;

namespace telegramBot.Source.Parser.ConcreteParser
{
    class TextParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document, IParserSettings settings)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll(settings.TagToParseBy);

            foreach (var item in items)
            {
                list.Add(item.TextContent);
            }

            return list.ToArray();
        }
    }
}
