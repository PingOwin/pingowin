using System;
using System.Threading.Tasks;

namespace PingIt.Lib
{
    public class ConsoleOutputter : IOutput
    {
        public Task SendToOutput(string text)
        {
            Console.WriteLine(text);
            return Task.FromResult(0);
        }
    }
}