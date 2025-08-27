using FluxorAppStateManagement.Domain.Events;
using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace FluxorAppStateManagement.Domain
{
    public class CounterBackend
    {
        private ConcurrentDictionary<Guid, int> counters = new();


        public ConcurrentDictionary<Guid, int> GetCounters() => counters;

        public bool IncrementCounter(Guid id)
        {
            if (counters.TryGetValue(id, out var currentCounter))
            {
                counters[id] = currentCounter + 1;
                return true;
            }

            return false;
        }

        public void AddNewCounter()
        {
            var id = Guid.NewGuid();

            counters[id] = 0;
        }
    }
}
