using FluxorAppStateManagement.State.Events.Notify;

namespace FluxorAppStateManagement.State.State
{
    public class CounterViewState : IProjectedApplicationState
    {
        public IReadOnlyList<Type> GetStates() => new List<Type>() { typeof(CounterState) };

        public CounterState CounterState { get; private set; } = new();

        public void ApplyNewState(EventArgs e)
        {
            if (e is NewCounterStateActionEvent newCounterStateActionEvent)
            {
                CounterState = newCounterStateActionEvent.ApplicationStateTransition(CounterState);
            }
        }
    }
}
