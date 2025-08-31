using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.Domain.Services
{
    public class WeatherService(WeatherBackend weatherBackend, EventBus.EventBus eventBus)
    {
        public event EventHandler<ReduceEventArgs> WeatherChanged;

        public void GetCities()
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(1000);
                eventBus.Publish<ReduceEventArgs>(new CityForecastEventArgs());
            });
        }

        public void GetForecasts(string city)
        {
            _ = Task.Run(async () =>
            {
                _ = weatherBackend.GetForecastFor(city);
                await Task.Delay(2000);
                eventBus.Publish<ReduceEventArgs>(new ForecastsEventArgs() { City = city });
            });
        }

        public void AddNewForecast(string city)
        {
            weatherBackend.AddNewForecast(city);
            eventBus.Publish<ReduceEventArgs>(new NewForecastEventArgs() { City = city });
        }

        public void UpdateForecast(string city)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(3000);
                weatherBackend.UpdateForecast(city);
                eventBus.Publish<ReduceEventArgs>(new NewForecastEventArgs() { City = city });
            });
        }

        public void AddCity(string city)
        {
            weatherBackend.AddCity(city);
            eventBus.Publish<ReduceEventArgs>(new CityForecastEventArgs() { City = city });
        }
    }
}
