using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telegramBot.Source.Parser.ConcreteParser
{
    class HtmlParserSettings : IParserSettings
    {
        public HtmlParserSettings(string url, string tagToParseBy)
        {
            TagToParseBy = tagToParseBy;
            BaseUrl = url;
        }

        public string BaseUrl { get; set; }

        public string PathSegment { get; set; }

        public string TagToParseBy { get; set; }

    }
}
