using Nancy;
using NServiceBus.Pipeline;

namespace NServiceBus.Diagnostics.Modules
{
    public class Root : NancyModule
    {
        public Root(PipelineExecutor pipelineExecutor)
        {
            Get["/"] = _ => View["index.html"];

            Get["/api"] = _ => Response.AsJson(new { incoming = pipelineExecutor.Incoming, outgoing = pipelineExecutor.Outgoing });
        }
    }
}