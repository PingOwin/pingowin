using System.Threading.Tasks;

namespace PingOwin.Core.Interfaces
{
    public interface IOutput
    {
        Task SendToOutput(string text);
    }
}