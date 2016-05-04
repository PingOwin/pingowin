using PingOwin.Core.Processing;

namespace PingOwin.Core.Interfaces
{
    public interface INotifierFactory
    {
        IOutput CreateNotifier();
    }
}