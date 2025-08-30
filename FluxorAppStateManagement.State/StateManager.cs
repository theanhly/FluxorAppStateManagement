using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State.Creator;
using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State
{
    public class StateManager
    {
        private readonly StateCreatorFactory stateCreatorFactory;

        public StateManager(StateCreatorFactory stateCreatorFactory)
        {
            this.stateCreatorFactory = stateCreatorFactory;
        }

        public void CreateProjectedApplicationStates(IProjectedApplicationState state)
        {
            var stateCreator = stateCreatorFactory.CreateCreator(state);
            stateCreator.Create();
        }


        public void CreateProjectedApplicationStates(IProjectedApplicationState state, ReduceEventArgs args)
        {
            var stateCreator = stateCreatorFactory.CreateCreator(state);
            args.InvokeStateCreator(stateCreator);
        }
    }
}