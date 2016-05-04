using System.Threading.Tasks;

namespace PingOwin.Core.Interfaces
{
    public interface INotify
    {
        Task Notify(string text);
    }
}