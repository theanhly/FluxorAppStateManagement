using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
namespace FluxorAppStateManagement.State
{
    public class StateManager
    {
        public event EventHandler<ReduceEventArgs> StateChanged;

        public StateManager(
            CounterService counterService,
            WeatherService weatherService)
        {
            counterService.CounterChanged += NotifyNewState;
            weatherService.WeatherChanged += NotifyNewState;
        }

        public void NotifyNewState(object sender, ReduceEventArgs args)
        {
            StateChanged?.Invoke(sender, args);
        }
    }
}