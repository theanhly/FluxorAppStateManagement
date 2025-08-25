using FluxorAppStateManagement.State.Events.Update;
using FluxorAppStateManagement.State.Factories;
using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State
{
    public class StateManager(CounterFactory counterFactory, WeatherFactory weatherFactory)
    {
        public event EventHandler<EventArgs> StateChanged;

        public void GetState(IProjectedApplicationState projectedStates)
        {
            foreach (var type in projectedStates.GetStates())
            {
                var args = EventArgs.Empty;

                if (type == typeof(CounterState))
                {
                    args = counterFactory.GetState();
                }
                else if (type == typeof(WeatherState))
                {
                    _ = GetForecastsAsync();
                }

                StateChanged?.Invoke(this, args);
            }
        }

        public void UpdateState(ActionEvent actionEvent)
        {
            var args = EventArgs.Empty;

            if (actionEvent is NewCounterActionEvent or IncrementCounterActionEvent)
            {
                args = counterFactory.UpdateState(actionEvent);
            }
            else if (actionEvent is NewWeatherActionEvent)
            {
                args = weatherFactory.UpdateState(actionEvent);
            }

            StateChanged?.Invoke(this, args);
        }

        private async Task GetForecastsAsync()
        {
            await Task.Delay(1000);
            var args = weatherFactory.GetState();
            StateChanged?.Invoke(this, args);
        }
    }
}
