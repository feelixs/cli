using System.Collections.Generic;
using SSoTme.OST.Lib.DataClasses;
using SSOTME.TestConApp.Root.TranspileHandlers;

namespace SassyMQ.SSOTME.Lib.RMQActors
{
    public interface IHandlerFactory
    {
        List<Transpiler> TranspilerList { get; }
        BaseHandler CreateHandler(SSOTMEPayload payload);
    }
}