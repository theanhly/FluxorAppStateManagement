using System.Collections.Concurrent;

namespace FluxorAppStateManagement.Domain.Events
{
    public class CounterEventArgs : ReduceEventArgs
    {
        public ConcurrentDictionary<Guid, int> Counters { get; set; }

        public override IReadOnlyList<IProjectedApplicationState> InvokeStateCreator(IProjectedStateCreator creator)
        {
            return creator.Create(this);
        }
    }
}
