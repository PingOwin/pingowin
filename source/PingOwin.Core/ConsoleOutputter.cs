using System;
using System.Threading.Tasks;

namespace PingOwin.Core
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