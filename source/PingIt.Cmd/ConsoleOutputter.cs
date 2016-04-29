using System;
using System.Threading.Tasks;

namespace PingIt.Cmd
{
    public class ConsoleOutputter : IOutput
    {
        public Task Output(string text)
        {
            Console.WriteLine(text);
            return Task.FromResult(0);
        }
    }
}