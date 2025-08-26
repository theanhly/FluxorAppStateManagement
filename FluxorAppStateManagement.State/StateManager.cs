using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State.Events.Update;
using FluxorAppStateManagement.State.Factories;
using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State
{
    public class StateManager
    {
        public event EventHandler<ReduceEventArgs> StateChanged;
        private readonly CounterFactory counterFactory;
        private readonly WeatherFactory weatherFactory;

        public StateManager(
            CounterFactory counterFactory,
            WeatherFactory weatherFactory,
            CounterService counterService,
            WeatherService weatherService)
        {
            this.counterFactory = counterFactory;
            this.weatherFactory = weatherFactory;

            counterService.CounterChanged += NotifyNewState;
            weatherService.WeatherChanged += NotifyNewState;
        }

        public void GetState(IProjectedApplicationState projectedStates)
        {
            foreach (var type in projectedStates.GetStates())
            {
                if (type == typeof(CounterState))
                {
                    counterFactory.GetState();
                }
                else if (type == typeof(WeatherState))
                {
                    _ = GetForecastsAsync();
                }
            }
        }

        public void UpdateState(ActionEvent actionEvent)
        {
            if (actionEvent is NewCounterActionEvent or IncrementCounterActionEvent)
            {
                counterFactory.UpdateState(actionEvent);
            }
            else if (actionEvent is NewWeatherActionEvent)
            {
                weatherFactory.UpdateState(actionEvent);
            }
        }

        public void NotifyNewState(object sender, ReduceEventArgs args)
        {
            StateChanged?.Invoke(sender, args);
        }

        private async Task GetForecastsAsync()
        {
            await Task.Delay(1000);
            weatherFactory.GetState();
        }
    }
}