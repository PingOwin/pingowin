using System;
using PingOwin.Core.Interfaces;
using PingOwin.Core.Notifiers.Konsole;
using PingOwin.Core.Notifiers.Slack;

namespace PingOwin.Core.Notifiers
{
    public class NotifierFactory : INotifierFactory
    {
        private readonly NotifierType _notifierType;
        private readonly ISlackOutputConfig _slackOutputConfig;

        public NotifierFactory(NotifierType notifierType, ISlackOutputConfig slackOutputConfig)
        {
            _notifierType = notifierType;
            _slackOutputConfig = slackOutputConfig;
        }

        public INotify CreateNotifier()
        {
            switch (_notifierType)
            {
                case NotifierType.Konsole:
                    return new ConsoleNotifier();
                case NotifierType.Slack:
                    return new SlackNotifier(_slackOutputConfig);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}