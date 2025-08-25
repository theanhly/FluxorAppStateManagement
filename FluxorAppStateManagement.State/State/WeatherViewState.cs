using FluxorAppStateManagement.State.Events.Notify;

namespace FluxorAppStateManagement.State.State
{
    public class WeatherViewState : IProjectedApplicationState
    {
        public IReadOnlyList<Type> GetStates() => new List<Type>() { typeof(CounterState), typeof(WeatherState) };

        public CounterState CounterState { get; set; } = new();

        public WeatherState WeatherState { get; set; } = new();

        public void ApplyNewState(EventArgs e)
        {
            if (e is NewCounterStateActionEvent newCounterStateActionEvent)
            {
                CounterState = newCounterStateActionEvent.ApplicationStateTransition(CounterState);
            }
            else if (e is NewWeatherStateActionEvent newWeatherStateActionEvent)
            {
                WeatherState = newWeatherStateActionEvent.ApplicationStateTransition(WeatherState);
            }
        }
    }
}
