using FluxorAppStateManagement.Domain;
using System.Collections.ObjectModel;
using FluxorAppStateManagement.State.Events.Notify;
using FluxorAppStateManagement.State.Events.Update;
using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State.Factories
{
    public class WeatherFactory(WeatherService weatherService)
    {
        public EventArgs GetState()
        {
            return new NewWeatherStateActionEvent()
            {
                ApplicationStateTransition = _ => new WeatherState()
                {
                    Forecasts = new ReadOnlyCollection<Weather>(weatherService.GetForecasts())
                }
            };
        }

        public EventArgs UpdateState(ActionEvent actionEvent)
        {
            if (actionEvent is NewWeatherActionEvent)
            {
                weatherService.AddNewForecast();
                return new NewWeatherStateActionEvent()
                {
                    ApplicationStateTransition = state => state with
                    {
                        Forecasts = new ReadOnlyCollection<Weather>(weatherService.GetForecasts())
                    }
                };
            }

            return EventArgs.Empty;
        }
    }
}
