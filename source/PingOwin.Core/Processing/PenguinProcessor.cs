using System.Linq;
using System.Threading.Tasks;
using PingOwin.Core.Interfaces;
using Serilog;

namespace PingOwin.Core.Processing
{
    public class PenguinProcessor
    {
        private readonly ILogger _log = Log.ForContext<PenguinProcessor>();

        private readonly IPenguinRepository _urlRepo;
        private readonly IPenguinResultsRepository _resultsRepo;
        private readonly ITransformResponses _transformer;
        private readonly HttpClientPinger _httpClientPinger;
        private readonly INotify _notifier;

        public PenguinProcessor(IPingConfiguration config, IPenguinRepository penguinRepoi, IPenguinResultsRepository resultsRepo, INotifierFactory notifierFactory, ITransformerFactory transformerfactory)
        {
            _httpClientPinger = new HttpClientPinger(config);
            _urlRepo = penguinRepoi;
            _resultsRepo = resultsRepo;

            _transformer = transformerfactory.CreateTransformer();
            _notifier = notifierFactory.CreateNotifier();
        }

        public async Task Tick()
        {
            var urlsToPing = await _urlRepo.GetAll();
            var urls = urlsToPing.Select(c => c.Url);
            var pingTasks = (from url in urls select _httpClientPinger.Ping(url)).ToList();
            var pingResults = await Task.WhenAll(pingTasks);
           
            foreach (var result in pingResults)
            {
                await _resultsRepo.Insert(new PenguinResult
                {
                    Url = result.Url,
                    ResponseTime = (int)result.ResponseTime
                });
            }

            var orderedByLevel = pingResults.OrderBy(c => c.Level).ToList();

            var transformedMsg = _transformer.Transform(orderedByLevel);
            await _notifier.Notify(transformedMsg);
            _log.Information(transformedMsg);
        }
    }
}
