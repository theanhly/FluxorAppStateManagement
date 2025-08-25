using System.Collections.ObjectModel;

namespace FluxorAppStateManagement.State.State
{
    public record CounterState
    {
        public IReadOnlyDictionary<Guid, int> Counters { get; init; } = ReadOnlyDictionary<Guid, int>.Empty;
    }
}
