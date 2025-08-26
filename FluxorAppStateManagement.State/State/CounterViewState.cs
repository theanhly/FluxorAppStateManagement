using System.Collections.ObjectModel;
using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.State.State
{
    public class CounterViewState : IProjectedApplicationState
    {
        public IReadOnlyList<Type> GetStates() => new List<Type>() { typeof(CounterState) };

        public CounterState CounterState { get; private set; } = new();

        public void Reduce(NewCountEventArgs args)
        {
            var newDict = CounterState.Counters.ToDictionary();
            newDict[args.Id] = args.Count;
            CounterState = CounterState with
            {
                Counters = new ReadOnlyDictionary<Guid, int>(newDict)
            };
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
        }

        public void Reduce(ForecastsEventArgs args)
        {
        }

        public void Reduce(CounterEventArgs args)
        {
            CounterState = CounterState with
            {
                Counters = args.Counters
            };
        }
    }
}
