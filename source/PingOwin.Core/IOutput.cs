using System.Threading.Tasks;

namespace PingOwin.Core
{
    public interface IOutput
    {
        Task SendToOutput(string text);
    }
}