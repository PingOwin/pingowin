using System.Collections.Generic;

namespace PingIt.Lib
{
    public interface ITransformResponses
    {
        string Transform(IEnumerable<PingResponse> responses);
        string TransformDebugInfo(string[] urls);
    }
}