using System;

namespace PingIt.Cmd
{
    public class ConsoleOutputter : IOutput
    {
        public void Output(string text)
        {
            Console.WriteLine(text);
        }
    }
}