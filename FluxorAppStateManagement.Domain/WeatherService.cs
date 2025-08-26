using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.Domain
{
    public class WeatherService
    {
        public event EventHandler<ReduceEventArgs> WeatherChanged;
        
        private List<Weather> forecasts = new();

        public void GetForecasts()
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(1000);
                WeatherChanged?.Invoke(this, new ForecastsEventArgs() { Forecasts = forecasts });
            });
        }

        public void AddNewForecast() {

            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var newWeather = new Weather()
            {
                Date = startDate.AddDays(forecasts.Count),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            };
            forecasts.Add(newWeather);

            WeatherChanged?.Invoke(this, new NewForecastEventArgs() { Weather = newWeather });
        }
    }
}
