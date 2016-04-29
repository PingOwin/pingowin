using System.Collections.Generic;
using Nancy;
using Nancy.Responses;

namespace PingIt.Cmd.WebHost
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get["/"] = x => new RedirectResponse("/allpinguins", RedirectResponse.RedirectType.Temporary);
            
            Get["/allpinguins"] = x =>
            {
                var allPenguinsModel = new PingOwinsModel();
                var pengs = new List<SinglePingOwin>
                {
                    new SinglePingOwin {Url = "https://www.aftenposten.no"}
                };
                allPenguinsModel.Penguins = pengs;
                return View["AllPingowins.sshtml", allPenguinsModel];
            }; ;
        }
    }

    public class PingOwinsModel 
    {
        public IEnumerable<SinglePingOwin> Penguins { get; set; } 
    }

    public class SinglePingOwin
    {
        public string Url { get; set; }
    }
}