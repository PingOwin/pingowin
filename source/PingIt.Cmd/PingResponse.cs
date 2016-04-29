namespace PingIt.Cmd
{
    public class PingResponse
    {
        public string Url { get; set; }
        public string ErrorMsg { get; set; }
        public long ResponseTime { get; set; }

        public Level Level { get; set; }
        public string StatusCodeText { get; set; }

        public bool HasErrors()
        {
            return Level == Level.Warn || Level == Level.Error;
        }
    }
}