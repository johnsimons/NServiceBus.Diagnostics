using System.Linq;
using Nancy;
using NServiceBus.Pipeline;

namespace NServiceBus.Diagnostics.Modules
{
    public class Root : NancyModule
    {
        public Root()
        {
            var pipelineBuilder = Configure.Instance.Builder.Build<PipelineBuilder>();

            var receivePhysicalMessageBehaviorList = pipelineBuilder.receivePhysicalMessageBehaviorList.Select(type => type.Name);
            var receiveLogicalMessageBehaviorList = pipelineBuilder.receiveLogicalMessageBehaviorList.Select(type => type.Name);
            var handlerInvocationBehaviorList = pipelineBuilder.handlerInvocationBehaviorList.Select(type => type.Name);
            var sendLogicalMessagesBehaviorList = pipelineBuilder.sendLogicalMessagesBehaviorList.Select(type => type.Name);
            var sendLogicalMessageBehaviorList = pipelineBuilder.sendLogicalMessageBehaviorList.Select(type => type.Name);
            var sendPhysicalMessageBehaviorList = pipelineBuilder.sendPhysicalMessageBehaviorList.Select(type => type.Name);

            Get["/"] = _ => View["index.html", new
            {
                ReceivePhysicalMessageBehaviors = receivePhysicalMessageBehaviorList,
                ReceiveLogicalMessageBehavior = receiveLogicalMessageBehaviorList,
                HandlerInvocationBehaviors = handlerInvocationBehaviorList,
                SendLogicalMessagesBehavior = sendLogicalMessagesBehaviorList,
                SendLogicalMessageBehaviors = sendLogicalMessageBehaviorList,
                SendPhysicalMessageBehaviors = sendPhysicalMessageBehaviorList
            }];
        }
    }
}