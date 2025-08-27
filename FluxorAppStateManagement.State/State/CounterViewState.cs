using FluxorAppStateManagement.Domain;

namespace FluxorAppStateManagement.State.State
{
    public record CounterViewState : IProjectedApplicationState
    {
        public IReadOnlyList<Type> GetStates() => new List<Type>() { typeof(CounterState) };

        public CounterState CounterState { get; init; } = new();
    }
}
