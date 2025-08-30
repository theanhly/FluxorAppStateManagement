using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State.State;
using System.Collections.ObjectModel;

namespace FluxorAppStateManagement.State.Creator
{
    public class CounterViewStateCreator(CounterBackend counterBackend, EventBus.EventBus eventBus) : IEventReducer
    {
        public void Create()
        {
            GetCounterViewState();
        }

        public void Create(NewCountEventArgs args)
        {
            GetCounterViewState();
        }

        public void Create(NewCounterEventArgs args)
        {
            GetCounterViewState();
        }

        public void Create(NewForecastEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Create(ForecastsEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Create(CounterEventArgs args)
        {
            GetCounterViewState();
        }

        public void Create(CityForecastEventArgs args)
        {
            new NotImplementedException();
        }

        private void GetCounterViewState()
        {
            var state = new CounterViewState()
            {
                CounterState = new CounterState()
                {
                    Counters = new ReadOnlyDictionary<Guid, int>(counterBackend.GetCounters())
                }
            };

            eventBus.Publish(new NewProjectedApplicationStateEventArgs() { NewState = state });
        }
    }
}
