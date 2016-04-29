using System.Linq;
using PingOwin;
using Serilog;

namespace PingIt.Lib.Processing
{
    public class PenguinProcessor
    {
        private readonly ILogger _log = Log.ForContext<PenguinProcessor>();

        private readonly Pinger _pinger;
        private readonly IPenguinRepository _repo;
        private readonly IPenguinResultsRepository _penguinResultsRepository;
        private readonly ITransformResponses _transformer;

        public PenguinProcessor(Pinger pinger, IPenguinRepository penguinRepoi, IPenguinResultsRepository penguinResultsRepository)
        {
            _pinger = pinger;
            _repo = penguinRepoi;
            _penguinResultsRepository = penguinResultsRepository;
            _transformer = new SlackMessageTransformer(Level.OK);
        }

        public void Tick()
        {
            var penguins = _repo.GetAll().GetAwaiter().GetResult();
            var urls = penguins.Where(c => c != null && !string.IsNullOrEmpty(c.Url)).Select(c => c.Url);
            if (urls != null)
            {
                var responses = _pinger.PingUrls(urls).GetAwaiter().GetResult();

                foreach (var result in responses)
                {
                    _penguinResultsRepository.Insert(new PenguinResult
                    {
                        Url = result.Url,
                        ResponseTime = (int) result.ResponseTime
                    });
                }
                var transformedMsg = _transformer.Transform(responses);
                _log.Information(transformedMsg);
            }
            else
            {
                _log.Information("null?!");
            }

        }
    }
}
