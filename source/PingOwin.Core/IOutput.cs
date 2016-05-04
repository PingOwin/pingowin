using System.Threading.Tasks;

namespace PingIt.Lib
{
    public interface IOutput
    {
        Task SendToOutput(string text);
    }
}