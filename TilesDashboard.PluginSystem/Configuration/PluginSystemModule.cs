using Autofac;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginSystem.Repositories;
using TilesDashboard.PluginSystem.Storage;

namespace TilesDashboard.PluginSystem.Configuration
{
    public class PluginSystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            PluginSystemMongoMapping.RegisterMappings();
            builder.RegisterType<PluginConfigRepository>().As<IPluginConfigRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PluginSystemStorage>().As<IPluginSystemStorage>().InstancePerLifetimeScope();
            builder.RegisterType<CancelletionTokenProvider>().As<ICancellationTokenProvider>().SingleInstance();
        }
    }
}
