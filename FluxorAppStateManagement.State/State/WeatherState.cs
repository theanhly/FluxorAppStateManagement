using FluxorAppStateManagement.Domain;

namespace FluxorAppStateManagement.State.State
{
    public record WeatherState
    {
        public IReadOnlyList<Weather> Forecasts { get; init; }
    }
}
