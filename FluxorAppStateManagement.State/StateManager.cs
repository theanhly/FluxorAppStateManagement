using System.Collections.ObjectModel;
using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State
{
    public class StateManager
    {
        public event EventHandler<NewProjectedApplicationStateEventArgs> StateChanged;
        private readonly CounterBackend counterBackend;
        private readonly WeatherBackend weatherBackend;
        private readonly StateCreatorFactory stateCreatorFactory;
        private IProjectedApplicationState state;

        public StateManager(
            CounterService counterService,
            WeatherService weatherService,
            CounterBackend counterBackend,
            WeatherBackend weatherBackend,
            StateCreatorFactory stateCreatorFactory)
        {
            this.stateCreatorFactory = stateCreatorFactory;
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
            var stateCreator = stateCreatorFactory.CreateCreator(state);

            StateChanged?.Invoke(sender, new NewProjectedApplicationStateEventArgs()
            {
                NewState = args.InvokeStateCreator(stateCreator)
            });
        }

        public IReadOnlyList<IProjectedApplicationState> Create(NewCountEventArgs args)
        {
            if (state is CounterViewState)
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
            if (state is CounterViewState)
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
            new CounterViewState()
            {
                CounterState = new CounterState()
                {
                    Counters = new ReadOnlyDictionary<Guid, int>(counterBackend.GetCounters())
                }
            };
            return new CounterState()
            {
                Counters = new ReadOnlyDictionary<Guid, int>(counterBackend.GetCounters())
            };
        }
    }
}