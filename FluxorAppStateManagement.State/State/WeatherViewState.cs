using FluxorAppStateManagement.Domain;

namespace FluxorAppStateManagement.State.State
{
    public class WeatherViewState : IProjectedApplicationState
    {
        public CounterState CounterState { get; set; } = new();

        public WeatherState WeatherState { get; set; } = new();
    }
}
