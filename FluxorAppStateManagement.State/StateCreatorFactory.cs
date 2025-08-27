using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State
{
    public class StateCreatorFactory(WeatherBackend weatherBackend, CounterBackend counterBackend)
    {
        public IProjectedStateCreator CreateCreator(IProjectedApplicationState state)
        {
            if (state is WeatherViewState weatherViewState)
            {
                return new WeatherViewStateCreator(weatherViewState, weatherBackend, counterBackend);
            }
            else if (state is CounterViewState)
            {
                return new CounterViewStateCreator(counterBackend);
            }

            return null;
        }
    }
}
