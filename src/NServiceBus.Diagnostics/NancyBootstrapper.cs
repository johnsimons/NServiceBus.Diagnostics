using System;
using System.IO;
using System.Reflection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
using NServiceBus.ObjectBuilder;
using NServiceBus.Pipeline;

namespace NServiceBus.Diagnostics
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        private readonly IBuilder _builder;

        public NancyBootstrapper(IBuilder builder)
        {
            _builder = builder;
        }

        
       protected override void ConfigureApplicationContainer(TinyIoCContainer container)
       {
           container.Register((_, __) => _builder.Build<PipelineExecutor>());

           base.ConfigureApplicationContainer(container);
       }
        

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