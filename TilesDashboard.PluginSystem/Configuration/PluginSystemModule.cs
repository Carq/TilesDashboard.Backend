using Autofac;
using TilesDashboard.Handy.Tools;

namespace TilesDashboard.PluginSystem.Configuration
{
    public class PluginSystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PluginConfigRepository>().As<IPluginConfigRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CancelletionTokenProvider>().As<ICancellationTokenProvider>().SingleInstance();
        }
    }
}
