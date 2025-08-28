using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State.State;
using System.Collections.ObjectModel;

namespace FluxorAppStateManagement.State
{
    public class CounterViewStateCreator(CounterBackend counterBackend) : IProjectedStateCreator
    {
        public IProjectedApplicationState Create()
        {
            return GetCounterViewState();
        }

        public IProjectedApplicationState Create(NewCountEventArgs args)
        {
            return GetCounterViewState();
        }

        public IProjectedApplicationState Create(NewCounterEventArgs args)
        {
            return GetCounterViewState();
        }

        public IProjectedApplicationState Create(NewForecastEventArgs args)
        {
            throw new NotImplementedException();
        }

        public IProjectedApplicationState Create(ForecastsEventArgs args)
        {
            throw new NotImplementedException();
        }

        public IProjectedApplicationState Create(CounterEventArgs args)
        {
            return GetCounterViewState();
        }

        public IProjectedApplicationState Create(CityForecastEventArgs args)
        {
            throw new NotImplementedException();
        }

        private CounterViewState GetCounterViewState()
        {
            return new CounterViewState()
            {
                CounterState = new CounterState()
                {
                    Counters = new ReadOnlyDictionary<Guid, int>(counterBackend.GetCounters())
                }
            };
        }
    }
}
