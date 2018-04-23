using Microsoft.Extensions.DependencyInjection;

namespace DoIt.PublishSubscribe
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPublishSubscribe(this IServiceCollection services)
        {
            services.AddTransient<IMessageHub, MessageHub>();
        }
    }
}