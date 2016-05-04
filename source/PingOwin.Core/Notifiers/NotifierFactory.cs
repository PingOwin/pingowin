using System;
using PingOwin.Core.Interfaces;
using PingOwin.Core.Notifiers.Konsole;
using PingOwin.Core.Notifiers.Slack;
using PingOwin.Core.Processing;

namespace PingOwin.Core.Notifiers
{
    public class NotifierFactory  : INotifierFactory
    {
        private readonly NotifierType _notifierType;
        private readonly Level _level;
        private readonly ISlackOutputConfig _slackOutputConfig;

        public NotifierFactory(NotifierType notifierType, Level level, ISlackOutputConfig slackOutputConfig)
        {
            _notifierType = notifierType;
            _level = level;
            _slackOutputConfig = slackOutputConfig;
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

        public IOutput CreateNotifier()
        {
            switch (_notifierType)
            {
                case NotifierType.Konsole:
                    return new ConsoleOutputter();
                case NotifierType.Slack:
                    return new SlackOutputter(_slackOutputConfig);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}