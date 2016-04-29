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
            var urls = penguins.Where(c => c != null && !string.IsNullOrEmpty(c.Url)).Select(c => c.Url);
            if (urls != null)
            {
                var responses = _pinger.PingUrls(urls).GetAwaiter().GetResult();

                //TODO: store responses to db


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
