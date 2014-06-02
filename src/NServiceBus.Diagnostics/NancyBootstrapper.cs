using System;
using System.IO;
using System.Reflection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
using NServiceBus.Pipeline;

namespace NServiceBus.Diagnostics
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        /*
       protected override void ConfigureApplicationContainer(TinyIoCContainer container)
       {
           //container.Register((_, __) =>
           //{
           //    Console.Out.WriteLine("Foo");
           //    return Configure.Instance.Builder.Build<PipelineBuilder>();
           //});

           base.ConfigureApplicationContainer(container);
           ResourceViewLocationProvider.RootNamespaces.Add(
             Assembly.GetAssembly(typeof(NancyBootstrapper)), "NServiceBus.Diagnostics.Views");
       }
        
       
       protected override NancyInternalConfiguration InternalConfiguration
       {
           get
           {
               return NancyInternalConfiguration.WithOverrides(configuration =>
               {
                   configuration.ViewLocationProvider = typeof (ResourceViewLocationProvider);
               });
           }
       }
       */

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
            conventions.StaticContentsConventions.Clear();
            conventions.StaticContentsConventions.AddDirectory("/js");
            conventions.StaticContentsConventions.AddDirectory("/css");
            conventions.StaticContentsConventions.AddDirectory("/fonts");
        }

#if DEBUG
        protected override IRootPathProvider RootPathProvider
        {
            get { return new CustomRootPathProvider(); }
        }

        class CustomRootPathProvider : IRootPathProvider
        {
            public string GetRootPath()
            {
                return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\NServiceBus.Diagnostics\\app"));
            }
        }
#endif
    }
}