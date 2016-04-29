using System.Collections.Generic;
using Nancy;
using Nancy.Responses;

namespace PingIt.Cmd.WebHost
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get["/"] = x => new RedirectResponse("/pingowins", RedirectResponse.RedirectType.Temporary);
            
            Get["/pingowins"] = x =>
            {
                var allPenguinsModel = new PingOwinsModel();
                var pengs = new List<SinglePingOwin>
                {
                    new SinglePingOwin {Url = "https://www.aftenposten.no"}
                };
                allPenguinsModel.Penguins = pengs;
                return View["AllPingowins.sshtml", allPenguinsModel];
            };

            Get["/results"] = x =>
            {
                var allResults = new PingResultsModel();
                var pengs = new List<SingleResult>
                {
                    new SingleResult {Url = "https://www.aftenposten.no"}
                };
                allResults.Results = pengs;
                return View["Results.sshtml", allResults];
            };
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
    public class PingResultsModel
    {
        public IEnumerable<SingleResult> Results { get; set; }
    }

    public class SingleResult
    {
        public string Url { get; set; }
    }
}