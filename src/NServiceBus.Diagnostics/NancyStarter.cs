using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using Nancy.Hosting.Self;
using NServiceBus.Config;

namespace NServiceBus.Diagnostics
{
    class NancyStarter:IWantToRunWhenBusStartsAndStops
    {
        private NancyHost host;

        public void Start()
        {
            var hostConfiguration = new HostConfiguration
            {
                UrlReservations =
                {
                    CreateAutomatically = false
                },
                UnhandledExceptionCallback = exception => Console.Out.WriteLine(exception),
            };

            var uri = string.Format("http://localhost:49152", SelectNextAvailablePort());

            host = new NancyHost(hostConfiguration, new Uri(uri));
            host.Start();

            Process.Start(uri);
        }

        public void Stop()
        {
            host.Dispose();
        }

        static int SelectNextAvailablePort()
        {
            var usedPorts = new List<int>();
            var r = new Random();
            int newPort;

            while (true)
            {
                using (var mListener = new HttpListener())
                {
                    newPort = r.Next(49152, 65535);
                    if (usedPorts.Contains(newPort))
                    {
                        continue;
                    }
                    mListener.Prefixes.Add(string.Format("http://localhost:{0}/", newPort));
                    try
                    {
                        mListener.Start();
                    }
                    catch
                    {
                        continue;
                    }
                }
                usedPorts.Add(newPort);
                break;
            }

            return newPort;
        }
    }
}
