using System.Linq;
using System.Threading.Tasks;
using PingOwin.Core.Interfaces;
using Serilog;

namespace PingOwin.Core.Processing
{
    public class PenguinProcessor
    {
        private readonly ILogger _log = Log.ForContext<PenguinProcessor>();

        private readonly IPenguinRepository _repo;
        private readonly IPenguinResultsRepository _penguinResultsRepository;
        private readonly ITransformResponses _transformer;
        private readonly HttpClientPinger _httpClientPinger;
        private IOutput _notifier;

        public PenguinProcessor(IPingConfiguration config, IPenguinRepository penguinRepoi, IPenguinResultsRepository penguinResultsRepository, INotifierFactory notifierFactory)
        {
            _httpClientPinger = new HttpClientPinger(config);
            _repo = penguinRepoi;
            _penguinResultsRepository = penguinResultsRepository;

            _transformer = notifierFactory.CreateTransformer(); //new SlackMessageTransformer(Level.OK);
            _notifier = notifierFactory.CreateNotifier();
        }

        public async Task Tick()
        {
            var penguins = await _repo.GetAll();
            var urls = penguins.Where(c => c != null && !string.IsNullOrEmpty(c.Url)).Select(c => c.Url);
            var pingTasks = (from url in urls select _httpClientPinger.Ping(url)).ToList();
            var pingResults = await Task.WhenAll(pingTasks);

            var orderedByLevel = pingResults.Where(c => c != null).OrderBy(c => c.Level).ToList();

            foreach (var result in orderedByLevel)
            {
                await _penguinResultsRepository.Insert(new PenguinResult
                {
                    Url = result.Url,
                    ResponseTime = (int)result.ResponseTime
                });
            }
            var transformedMsg = _transformer.Transform(orderedByLevel);
            await _notifier.SendToOutput(transformedMsg);
            _log.Information(transformedMsg);
        }
    }

    public interface INotifierFactory
    {
        ITransformResponses CreateTransformer();
        IOutput CreateNotifier();
    }
}
