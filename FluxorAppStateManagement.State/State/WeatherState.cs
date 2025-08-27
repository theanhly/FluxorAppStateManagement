using System.Collections.ObjectModel;
using FluxorAppStateManagement.Domain;

namespace FluxorAppStateManagement.State.State
{
    public record WeatherState
    {
        public IReadOnlyList<string> Cities { get; init; } = ReadOnlyCollection<string>.Empty;

        public string City { get; set; }

        public IReadOnlyList<Weather> Forecasts { get; init; }

        public IReadOnlyList<Weather> RegionalForcasts { get; init; }
    }
}
