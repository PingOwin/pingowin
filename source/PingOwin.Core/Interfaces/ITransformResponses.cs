using System.Collections.Generic;
using PingOwin.Core.Processing;

namespace PingOwin.Core.Interfaces
{
    public interface ITransformResponses
    {
        string Transform(IEnumerable<PingResponse> responses);
        string TransformDebugInfo(IEnumerable<string> urls);
    }
}