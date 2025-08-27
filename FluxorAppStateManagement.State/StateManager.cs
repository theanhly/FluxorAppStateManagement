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
            var counterState = new CounterState()
            {
                Counters = new ReadOnlyDictionary<Guid, int>(counterBackend.GetCounters())
            };
            return
            [
                new CounterViewState()
                {
                    CounterState = counterState
                }
            ];
        }

        public IReadOnlyList<IProjectedApplicationState> Create(NewCounterEventArgs args)
        {
            var counterState = new CounterState()
            {
                Counters = new ReadOnlyDictionary<Guid, int>(counterBackend.GetCounters())
            };
            return
            [
                new CounterViewState()
                {
                    CounterState = counterState
                }
            ];
        }

        public IReadOnlyList<IProjectedApplicationState> Create(NewForecastEventArgs args)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IProjectedApplicationState> Create(ForecastsEventArgs args)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IProjectedApplicationState> Create(CounterEventArgs args)
        {
            var counterState = new CounterState()
            {
                Counters = new ReadOnlyDictionary<Guid, int>(counterBackend.GetCounters())
            };
            return
            [
                new CounterViewState()
                {
                    CounterState = counterState
                }
            ];
        }

        public IReadOnlyList<IProjectedApplicationState> Create(CityForecastEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}