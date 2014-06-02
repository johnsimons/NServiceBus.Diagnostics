using Nancy;
using NServiceBus.Pipeline;

namespace NServiceBus.Diagnostics.Modules
{
    public class Root : NancyModule
    {
        public Root()
        {
            

            Get["/"] = _ => View["index.html"];

            Get["/api"] = _ =>
            {
                var pipelineBuilder = Configure.Instance.Builder.Build<PipelineExecutor>();

                return Response.AsJson(new {incoming = pipelineBuilder.Incoming, outgoing = pipelineBuilder.Outgoing});
            };
        }
    }
}