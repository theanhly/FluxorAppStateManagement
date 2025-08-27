using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.Domain
{
    public class WeatherBackend
    {

        private Dictionary<string, List<Weather>> forecasts = new();

        public Dictionary<string, List<Weather>> GetAllForecasts() => forecasts;

        public List<string> GetCities()
        {
            if (forecasts.Count > 0)
            {
                return forecasts.Keys.ToList();
            }

            return new List<string>();
        }

        public List<Weather> GetForecastFor(string city)
        {
            if (forecasts.TryGetValue(city, out var cityForecasts))
            {
                return cityForecasts;
            }

            return new List<Weather>();
        }

        public void AddCity(string city)
        {
            if (!forecasts.ContainsKey(city))
            {
                forecasts[city] = new List<Weather>();
            }
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
        }
    }
}
