using FluxorAppStateManagement.Domain.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluxorAppStateManagement.Domain;

namespace FluxorAppStateManagement.State.State
{
    public class WeatherViewState : IProjectedApplicationState
    {
        public IReadOnlyList<Type> GetStates() => new List<Type>() { typeof(CounterState), typeof(WeatherState) };

        public CounterState CounterState { get; private set; } = new();

        public WeatherState WeatherState { get; private set; } = new();

        public void Reduce(NewCountEventArgs args)
        {
        }

        public void Reduce(NewCounterEventArgs args)
        {
            var newDict = CounterState.Counters.ToDictionary();
            newDict[args.Id] = args.Count;
            CounterState = CounterState with
            {
                Counters = new ReadOnlyDictionary<Guid, int>(newDict)
            };
        }

        public void Reduce(NewForecastEventArgs args)
        {
            var list = WeatherState.Forecasts?.ToList() ?? new();

            list.Add(args.Weather);

            WeatherState = WeatherState with { Forecasts = list };
        }

        public void Reduce(ForecastsEventArgs args)
        {
            WeatherState = WeatherState with { Forecasts = new ReadOnlyCollection<Weather>(args.Forecasts.ToList()) };
        }

        public void Reduce(CounterEventArgs args)
        {
            CounterState = CounterState with { Counters = args.Counters };
        }
    }
}
