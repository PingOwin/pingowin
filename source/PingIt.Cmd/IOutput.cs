using System.Threading.Tasks;

namespace PingIt.Cmd
{
    public interface IOutput
    {
        Task Output(string text);
    }
}