using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.Domain
{
    public class WeatherService
    {
        public event EventHandler<ReduceEventArgs> WeatherChanged;

        private Dictionary<string, List<Weather>> forecasts = new();

        public void GetCities()
        {
            _ = Task.Run(async () =>
            {
                if (this.forecasts.Count != 0)
                {
                    await Task.Delay(1000);
                    WeatherChanged?.Invoke(this, new CityForecastEventArgs() { Cities = this.forecasts.Keys.ToList() });
                }
            });
        }

        public void GetForecasts(string city)
        {
            _ = Task.Run(async () =>
            {
                if (this.forecasts.TryGetValue(city, out var forecasts))
                {
                    await Task.Delay(2000);
                    WeatherChanged?.Invoke(this, new ForecastsEventArgs() { Forecasts = forecasts, RegionalForecasts = forecasts });
                }
            });
        }

        public void AddNewForecast(string city)
        {
            var forecasts = new List<Weather>();

            if (this.forecasts.TryGetValue(city, out var foundForecasts))
            {
                forecasts = foundForecasts;
            }

            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var newWeather = new Weather()
            {
                City = city,
                Date = startDate.AddDays(forecasts.Count),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            };
            forecasts.Add(newWeather);

            this.forecasts[city] = forecasts;

            WeatherChanged?.Invoke(this, new NewForecastEventArgs() { Weather = newWeather });
        }

        public void AddCity(string city)
        {
            if (!forecasts.ContainsKey(city))
            {
                forecasts[city] = new List<Weather>();
                WeatherChanged?.Invoke(this, new CityForecastEventArgs() { Cities = forecasts.Keys.ToList() });
            }
        }
    }
}
