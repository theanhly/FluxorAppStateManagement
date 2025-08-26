using System.Collections.Concurrent;
using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.Domain
{
    public class CounterService
    {
        public event EventHandler<ReduceEventArgs> CounterChanged;
        
        private ConcurrentDictionary<Guid, int> counter = new();

        public void GetCounters()
        {
            CounterChanged?.Invoke(this, new CounterEventArgs() { Counters = counter});
        }

        public bool IncrementCounter(Guid id)
        {
            if (counter.TryGetValue(id, out var currentCounter))
            {
                counter[id] = currentCounter + 1;
                CounterChanged?.Invoke(this, new NewCountEventArgs() { Id = id, Count = counter[id] });
                return true;
            }

            return false;
        }

        public void AddNewCounter()
        {
            var id = Guid.NewGuid();

            counter[id] = 0;
            CounterChanged?.Invoke(this, new NewCounterEventArgs() { Id = id, Count = counter[id] });
        }
    }
}
