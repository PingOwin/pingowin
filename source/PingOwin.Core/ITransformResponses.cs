using System.Collections.Generic;

namespace PingOwin.Core
{
    public interface ITransformResponses
    {
        string Transform(IEnumerable<PingResponse> responses);
        string TransformDebugInfo(IEnumerable<string> urls);
    }
}