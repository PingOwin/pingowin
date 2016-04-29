using System.Linq;
using Serilog;

namespace PingIt.Lib.Processing
{
    public class PenguinProcessor
    {
        private readonly ILogger _log = Log.ForContext<PenguinProcessor>();
        private Pinger _pinger;
        private IPenguinRepository _repo;
        private ITransformResponses _transformer;

        public PenguinProcessor(IPingConfiguration config, IPenguinRepository penguinRepoi)
        {
            _pinger = new Pinger(config);
            _repo = penguinRepoi;
            _transformer = new SlackMessageTransformer(Level.OK);
        }

        public void Tick()
        {
            var penguins = _repo.GetAll().GetAwaiter().GetResult();
            var urls = penguins.Select(c => c.Url);
            var responses = _pinger.PingUrls(urls).GetAwaiter().GetResult();

            //TODO: store responses to db


            var transformedMsg = _transformer.Transform(responses);
            _log.Information(transformedMsg);
        }
    }
}
