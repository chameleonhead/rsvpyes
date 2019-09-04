using RsvpYes.Domain.Users;
using System;

namespace RsvpYes.Domain.Messaging
{
    public class RequestMessage : MessageBase
    {
        public RequestMessage(User from, User to, string titleText, string message, string responseUrl)
            : base(from, to)
        {
            TitleText = titleText;
            Message = message;
            ResponseUrl = responseUrl;
        }

        public string TitleText { get; }
        public string Message { get; }
        public string ResponseUrl { get; }

        protected override string RenderTitle()
        {
            return TitleText;
        }

        protected override string RenderBody()
        {
            var signature = From.MessageSignature != null
                ? string.Concat(Environment.NewLine, From.MessageSignature, Environment.NewLine)
                : "";
            return string.Concat($"{To.Name} 様", Environment.NewLine,
                Environment.NewLine,
                Message, Environment.NewLine,
                Environment.NewLine,
                $"回答は以下のURLよりお願いいたします。", Environment.NewLine,
                ResponseUrl, Environment.NewLine,
                Environment.NewLine,
                "以上", Environment.NewLine,
                signature);
        }
    }
}