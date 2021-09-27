namespace telegramBot.Source.Parser
{
    interface IParserSettings
    {
        string BaseUrl { get; set; }

        string PathSegment { get; set; }

        string TagToParseBy { get; set; }
    }
}
