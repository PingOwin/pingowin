using System;

namespace PingOwin.Core.Processing
{
    public class PenguinResult
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ResponseTime { get; set; }
        public DateTime? TimeStamp { get;set;}
    }
}