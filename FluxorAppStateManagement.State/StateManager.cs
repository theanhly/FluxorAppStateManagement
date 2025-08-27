using System.Collections.ObjectModel;
using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State
{
    public class StateManager : IProjectedStateCreator
    {
        public event EventHandler<NewProjectedApplicationStateEventArgs> StateChanged;
        private readonly CounterBackend counterBackend;
        private readonly WeatherBackend weatherBackend;
        private IProjectedApplicationState state;

        public StateManager(
            CounterService counterService,
            WeatherService weatherService,
            CounterBackend counterBackend,
            WeatherBackend weatherBackend)
        {
            this.weatherBackend = weatherBackend;
            this.counterBackend = counterBackend;

            counterService.CounterChanged += CreateProjectedApplicationStates;
            weatherService.WeatherChanged += CreateProjectedApplicationStates;
        }

        public void UpdateState(IProjectedApplicationState state, Action action)
        {
            this.state = state;

            action?.Invoke();
        }

        private void CreateProjectedApplicationStates(object sender, ReduceEventArgs args)
        {
            foreach (var newState in args.InvokeStateCreator(this))
            {
                StateChanged?.Invoke(sender, new NewProjectedApplicationStateEventArgs()
                {
                    NewState = newState
                });
            }
        }

        public IReadOnlyList<IProjectedApplicationState> Create(NewCountEventArgs args)
        {
            if (state is WeatherViewState weatherViewState)
            {
                var newWeatherViewState = new WeatherViewState()
                {
                    CounterState = GetCounterState(),
                    WeatherState = GetWeatherStateFor(weatherViewState.WeatherState.City)
                };

                return [newWeatherViewState];
            }
            else if (state is CounterViewState)
            {
                return
                [
                    new CounterViewState()
                    {
                        CounterState = GetCounterState()
                    }
                ];
            }

            return [];
        }

        public IReadOnlyList<IProjectedApplicationState> Create(NewCounterEventArgs args)
        {
            if (state is WeatherViewState weatherViewState)
            {
                var newWeatherViewState = new WeatherViewState()
                {
                    CounterState = GetCounterState(),
                    WeatherState = GetWeatherStateFor(weatherViewState.WeatherState.City)
                };

                return [newWeatherViewState];
            }
            else if (state is CounterViewState)
            {
                return
                [
                    new CounterViewState()
                    {
                        CounterState = GetCounterState()
                    }
                ];
            }

            return [];
        }

        public IReadOnlyList<IProjectedApplicationState> Create(NewForecastEventArgs args)
        {
            if (state is WeatherViewState)
            {
                var weatherViewState = new WeatherViewState()
                {
                    CounterState = GetCounterState(),
                    WeatherState = GetWeatherStateFor(args.City)
                };

                return [weatherViewState];
            }

            return [];
        }

        public IReadOnlyList<IProjectedApplicationState> Create(ForecastsEventArgs args)
        {
            if (state is WeatherViewState)
            {
                var weatherViewState = new WeatherViewState()
                {
                    CounterState = GetCounterState(),
                    WeatherState = GetWeatherStateFor(args.City)
                };

                return [weatherViewState];
            }

            return [];
        }

        public IReadOnlyList<IProjectedApplicationState> Create(CounterEventArgs args)
        {
            return
            [
                new CounterViewState()
                {
                    CounterState = GetCounterState()
                }
            ];
        }

        public IReadOnlyList<IProjectedApplicationState> Create(CityForecastEventArgs args)
        {
            if (state is WeatherViewState)
            {
                var weatherViewState = new WeatherViewState()
                {
                    CounterState = GetCounterState(),
                    WeatherState = GetWeatherStateFor(args.City)
                };

                return [weatherViewState];
            }
            return [];
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

        public CounterState GetCounterState()
        {
            return new CounterState()
            {
                Counters = new ReadOnlyDictionary<Guid, int>(counterBackend.GetCounters())
            };
        }
    }
}