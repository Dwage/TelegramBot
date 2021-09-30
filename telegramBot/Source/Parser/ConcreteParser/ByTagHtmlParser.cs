using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;

namespace telegramBot.Source.Parser.ConcreteParser
{
    class ByTagHtmlParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document, IParserSettings settings)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll(settings.TagToParseBy);

            if (settings.TagToParseBy.Contains("img"))
            {
                foreach (var item in items)
                {
                    list.Add(item.GetAttribute("src"));
                }
            }
            else
            {
                foreach (var item in items)
                {
                    list.Add(item.TextContent);
                }
            }

            return list.ToArray();
        }
    }
}
