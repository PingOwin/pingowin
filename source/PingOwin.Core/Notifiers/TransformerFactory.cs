using PingOwin.Core.Interfaces;
using PingOwin.Core.Notifiers.Slack;

namespace PingOwin.Core.Notifiers
{
    public class TransformerFactory : ITransformerFactory
    {
        private readonly NotifierType _notifierType;
        private readonly Level _level;

        public TransformerFactory(NotifierType notifierType, Level level)
        {
            _notifierType = notifierType;
            _level = level;
        }

        public ITransformResponses CreateTransformer()
        {
            switch (_notifierType)
            {
                case NotifierType.Konsole:
                    return new SlackMessageTransformer(_level);
                case NotifierType.Slack:
                    return new SlackMessageTransformer(_level);
                default:
                    return new SlackMessageTransformer(_level);
            }
        }
    }
}