using System.Collections.Concurrent;

namespace FluxorAppStateManagement.Domain.Events
{
    public class CounterEventArgs : ReduceEventArgs
    {
        public ConcurrentDictionary<Guid, int> Counters { get; set; }

        public override void InvokeReducer(IProjectedApplicationState applicationState)
        {
            applicationState.Reduce(this);
        }
    }
}
