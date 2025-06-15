using KristofferStrube.Blazor.FileAPI.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KristofferStrube.Blazor.Streams
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddStreams(this IServiceCollection serviceCollection, Action<StreamsOptions>? configure)
        {
            serviceCollection.ConfigureStreamOptions(configure);
            return serviceCollection;
        }

        private static void ConfigureStreamOptions(this IServiceCollection services, Action<StreamsOptions>? configure)
        {
            if (configure is null) return;

            services.Configure(configure);
            configure(StreamsOptions.DefaultInstance);
        }
    }
}