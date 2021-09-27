using AngleSharp.Html.Dom;

namespace telegramBot.Source.Parser
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document, IParserSettings settings);
    }
}
