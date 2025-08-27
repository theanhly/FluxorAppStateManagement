using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.State
{
    public class StateManager
    {
        public event EventHandler<NewProjectedApplicationStateEventArgs> StateChanged;
        private readonly StateCreatorFactory stateCreatorFactory;
        private IProjectedApplicationState state;

        public StateManager(
            CounterService counterService,
            WeatherService weatherService,
            StateCreatorFactory stateCreatorFactory)
        {
            this.stateCreatorFactory = stateCreatorFactory;

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
            var newState = args.InvokeStateCreator(stateCreator);
            state = newState;
            StateChanged?.Invoke(sender, new NewProjectedApplicationStateEventArgs()
            {
                NewState = newState
            });
        }
    }
}