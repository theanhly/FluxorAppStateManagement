using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State.Creator
{
    public class StateCreatorFactory(WeatherBackend weatherBackend, CounterBackend counterBackend, EventBus.EventBus eventBus)
    {
        public IEventReducer CreateCreator(IProjectedApplicationState state)
        {
            if (state is WeatherViewState weatherViewState)
            {
                return new WeatherViewStateCreator(weatherViewState, weatherBackend, counterBackend, eventBus);
            }
            else if (state is CounterViewState)
            {
                return new CounterViewStateCreator(counterBackend, eventBus);
            }

            return null;
        }
    }
}
