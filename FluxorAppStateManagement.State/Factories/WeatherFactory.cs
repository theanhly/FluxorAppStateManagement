using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.State.Events.Update;

namespace FluxorAppStateManagement.State.Factories
{
    public class WeatherFactory
    {
        private readonly WeatherService weatherService;

        public WeatherFactory(WeatherService weatherService)
        {
            this.weatherService = weatherService;
        }
        public void GetState()
        {
            weatherService.GetForecasts();
        }

        public void UpdateState(ActionEvent actionEvent)
        {
            if (actionEvent is NewWeatherActionEvent)
            {
                weatherService.AddNewForecast();
            }
        }
    }
}
