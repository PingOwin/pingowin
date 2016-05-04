using System;
using System.Threading.Tasks;
using PingOwin.Core.Interfaces;

namespace PingOwin.Core.Notifiers.Konsole
{
    public class ConsoleNotifier : INotify
    {
        public Task Notify(string text)
        {
            Console.WriteLine(text);
            return Task.FromResult(0);
        }
    }
}