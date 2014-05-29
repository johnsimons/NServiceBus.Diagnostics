
using System;
using System.Net.Cache;

namespace Endpoint
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
    }

    class Sender : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Bus.SendLocal(new Request());
        }

        public void Stop()
        {
        }
    }

    class RequestHandler : IHandleMessages<Request>
    {
        public IBus Bus { get; set; }
        public void Handle(Request message)
        {
            Bus.Reply(new Response());
        }
    }

    class ResponseHandler : IHandleMessages<Response>
    {
        public void Handle(Response message)
        {
            Console.Out.WriteLine("Got response");
        }
    }

    class Request: ICommand
    {
    }

    class Response : IMessage
    {
    }
}
