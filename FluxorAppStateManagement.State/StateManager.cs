using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.State
{
    public class StateManager
    {
        public event EventHandler<NewProjectedApplicationStateEventArgs> StateChanged;
        private readonly StateCreatorFactory stateCreatorFactory;

        public StateManager(StateCreatorFactory stateCreatorFactory)
        {
            this.stateCreatorFactory = stateCreatorFactory;
        }

        public void CreateProjectedApplicationStates(IProjectedApplicationState state)
        {
            var stateCreator = stateCreatorFactory.CreateCreator(state);
            StateChanged?.Invoke(this, new NewProjectedApplicationStateEventArgs()
            {
                NewState = stateCreator.Create()
            });
        }


        public void CreateProjectedApplicationStates(IProjectedApplicationState state, ReduceEventArgs args)
        {
            var stateCreator = stateCreatorFactory.CreateCreator(state);
            StateChanged?.Invoke(this, new NewProjectedApplicationStateEventArgs()
            {
                NewState = args.InvokeStateCreator(stateCreator)
            });
        }
    }
}