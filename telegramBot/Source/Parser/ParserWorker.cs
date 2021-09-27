using AngleSharp.Html.Parser;
using System;
using System.Threading;

namespace telegramBot.Source.Parser
{
    class ParserWorker<T> where T : class
    {
        private IParser<T> parser;
        private IParserSettings parserSettings;
        private HtmlLoader loader;
        private bool isActive;

        #region Properties

        public IParser<T> Parser { get; set; }

        public IParserSettings Settings
        {
            get { return parserSettings; }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive => isActive;

        #endregion

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public ParserWorker(IParser<T> parser) => this.parser = parser;

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort() => isActive = false;

        private async void Worker()
        {
            HtmlParser domParser = new HtmlParser();

            if (!isActive)
            {
                OnCompleted?.Invoke(this);
                return;
            }

            string source = await loader.GetSource();
            ;

            var document = await domParser.ParseDocumentAsync(source);

            T result = parser.Parse(document, Settings);

            OnNewData?.Invoke(this, result);


            OnCompleted?.Invoke(this);
            isActive = false;
        }

    }
}
