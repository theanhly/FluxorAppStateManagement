using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State.State;
using System.Collections.ObjectModel;

namespace FluxorAppStateManagement.State
{
    public class WeatherViewStateCreator(WeatherViewState state, WeatherBackend weatherBackend, CounterBackend counterBackend)
        : IProjectedStateCreator
    {
        public IProjectedApplicationState Create()
        {
            return GetState(string.Empty);
        }

        public IProjectedApplicationState Create(NewCountEventArgs args)
        {
            throw new NotImplementedException();
        }

        public IProjectedApplicationState Create(NewCounterEventArgs args)
        {
            return GetState(state.WeatherState.City);
        }

        public IProjectedApplicationState Create(NewForecastEventArgs args)
        {
            return GetState(args.City);
        }

        public IProjectedApplicationState Create(ForecastsEventArgs args)
        {
            return GetState(args.City);
        }

        public IProjectedApplicationState Create(CounterEventArgs args)
        {
            return null;
        }

        public IProjectedApplicationState Create(CityForecastEventArgs args)
        {
            return GetState(args.City);
        }

        private WeatherViewState GetState(string city)
        {
            return new WeatherViewState()
            {
                CounterState = new CounterState()
                {
                    Counters = new ReadOnlyDictionary<Guid, int>(counterBackend.GetCounters())
                },
                WeatherState = GetWeatherStateFor(city)
            };
        }

        private WeatherState GetWeatherStateFor(string city)
        {
            var weatherState = new WeatherState()
            {
                Cities = new ReadOnlyCollection<string>(weatherBackend.GetCities()),
            };

            if (!string.IsNullOrEmpty(city))
            {
                weatherState = weatherState with
                {
                    City = city,
                    Forecasts = weatherBackend.GetForecastFor(city),
                    RegionalForcasts = weatherBackend.GetForecastFor(city)
                };
            }

            return weatherState;
        }
    }
}
