namespace PingIt.Cmd
{
    public interface ISlackOutputConfig
    {
        string Token { get; }
        string Channel { get; }
        string IconUrl { get; }
        string Username { get; }
    }
}