/*using AngleSharp.Html.Dom;
using System.Collections.Generic;

namespace telegramBot.Source.Parser.ConcreteParser
{
    class ImageParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document, IParserSettings settings)
        {
            List<string> list = new List<string>();

            string searchOptions;
            if (settings.TagToParseBy != null && settings.TagToParseBy != string.Empty)
                searchOptions = $"{settings.TagToParseBy} > img";
            else
                searchOptions = "img";

            var items = document.QuerySelectorAll(searchOptions);

            foreach (var item in items)
            {
                list.Add(item.GetAttribute("src"));
            }

            return list.ToArray();
        }
    }
    
}*/
