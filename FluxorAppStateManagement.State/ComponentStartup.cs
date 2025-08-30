using FluxorAppStateManagement.State.Creator;
using Microsoft.Extensions.DependencyInjection;

namespace FluxorAppStateManagement.State
{
    public static class ComponentStartup
    {
        public static void InitializeState(this IServiceCollection services)
        {
            services.AddSingleton<StateManager>();
            services.AddSingleton<StateCreatorFactory>();

        }
    }
}
