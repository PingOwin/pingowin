using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using PingOwin.Core.Interfaces;

namespace PingOwin.Core.Frontend
{
    public class RootModule : NancyModule
    {
        private readonly IPenguinResultsRepository _resultsRepo;
        private readonly IPenguinRepository _penguinRepository;

        public RootModule(IPenguinRepository penguinRepository, IPenguinResultsRepository resultsRepo)
        {
            _resultsRepo = resultsRepo;
            Get["/"] = async (x,t) =>
            {
                return new RedirectResponse("/pingowins", RedirectResponse.RedirectType.Temporary);
            };
            
            Get["/pingowins"] = async (x, t) =>
            {
                var all = await penguinRepository.GetAll();
                var allPenguinsModel = new PingOwinsModel();
                var pengs = all.Select(c => new SinglePingOwin {Url = c.Url});
                allPenguinsModel.Penguins = pengs;
                return View["AllPingowins.sshtml", allPenguinsModel];
            };

            Get["/results"] = async (x,t) =>
            {
                var filter = this.Bind<ResultsQueryFilter>();

                if (filter == null || filter.Take <= 0)
                {
                    return new RedirectResponse("/results?skip=0&take=20");
                }
                var allResults = await _resultsRepo.GetAll(filter.Skip,filter.Take);
                var resultsModel = new PingResultsModel();
                var results = allResults.Select(c =>  new SingleResult
                {
                    Url = c.Url,
                    ResponseTime = c.ResponseTime.ToString(),
                    TimeStamp = c.TimeStamp?.ToString("yyyy-MM-dd HH:MM:ss") ?? "Undefined"
                });
                resultsModel.Results = results;
                return View["Results.sshtml", resultsModel];
            };
        }
    }

    public class ResultsQueryFilter
    {
        public int Skip { get; set; }
        public int Take { get; set; }
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
        public string ResponseTime { get; set; }
        public string TimeStamp { get; set; }

       
    }
}