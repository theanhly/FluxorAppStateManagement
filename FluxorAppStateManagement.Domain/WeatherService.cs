using System;
using System.Linq;

namespace FluxorAppStateManagement.Domain
{
    public class WeatherService
    {
        private List<Weather> forecasts = new();

        public List<Weather> GetForecasts() => forecasts;

        public void AddNewForecast() {

            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            forecasts.Add(new Weather()
            {
                Date = startDate.AddDays(forecasts.Count),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            });
        }
    }
}
