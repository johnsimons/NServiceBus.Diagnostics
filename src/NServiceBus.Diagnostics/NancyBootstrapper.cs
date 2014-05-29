using System;
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

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            //conventions.StaticContentsConventions.Clear();
            //conventions.StaticContentsConventions.Add((ctx, root) =>
            //{
            //    var reqPath = ctx.Request.Path;

            //    reqPath = "Content" + reqPath.Replace('/', '\\');

            //    return new SpecialEmbeddedFileResponse(
            //        GetType().Assembly,
            //        reqPath);
            //});
        }

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration {Password = @"Welcome1"}; }
        }
    }
}