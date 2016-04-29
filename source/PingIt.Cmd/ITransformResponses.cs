using System.Collections.Generic;

namespace PingIt.Cmd
{
    public interface ITransformResponses
    {
        string Transform(IEnumerable<PingResponse> responses);
        string TransformDebugInfo(string[] urls);
    }
}