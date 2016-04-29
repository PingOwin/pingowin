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
                var allPenguinsModel = new PenguinsModel();
                var pengs = new List<SinglePenguin>();
                pengs.Add(new SinglePenguin { Url = "https://www.aftenposten.no" });
                allPenguinsModel.Penguins = pengs;
                return View["AllPenguins.sshtml", allPenguinsModel];
            }; ;
        }
    }

    public class PenguinsModel 
    {
        public IEnumerable<SinglePenguin> Penguins { get; set; } 
    }

    public class SinglePenguin
    {
        public string Url { get; set; }
    }
}