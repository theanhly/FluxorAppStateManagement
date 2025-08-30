using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State.State;
using System.Collections.ObjectModel;

namespace FluxorAppStateManagement.State.Creator
{
    public class WeatherViewStateCreator(WeatherViewState state, WeatherBackend weatherBackend, CounterBackend counterBackend, EventBus.EventBus eventBus)
        : IEventReducer
    {
        public void Create()
        {
            GetState(string.Empty);
        }

        public void Create(NewCountEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Create(NewCounterEventArgs args)
        {
            GetState(state.WeatherState.City);
        }

        public void Create(NewForecastEventArgs args)
        {
            // if the state is currently displaying that city then it makes sense to update the projected state. Otherwise the user would be displayed inconsistent
            // state with what he selected.
            if (state.WeatherState.City == args.City)
            {
                GetState(args.City);
            }
        }

        public void Create(ForecastsEventArgs args)
        {
            GetState(args.City);
        }

        public void Create(CounterEventArgs args)
        {
        }

        public void Create(CityForecastEventArgs args)
        {
            GetState(args.City);
        }

        private void GetState(string city)
        {
            var state = new WeatherViewState()
            {
                CounterState = new CounterState()
                {
                    Counters = new ReadOnlyDictionary<Guid, int>(counterBackend.GetCounters())
                },
                WeatherState = GetWeatherStateFor(city)
            };

            eventBus.Publish(new NewProjectedApplicationStateEventArgs() { NewState = state });
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
                    RegionalForcasts = weatherBackend.GetRegionalForecastFor(city)
                };
            }

            return weatherState;
        }
    }
}
