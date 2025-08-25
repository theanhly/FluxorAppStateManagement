using System.Collections.Concurrent;

namespace FluxorAppStateManagement.Domain
{
    public class CounterService
    {
        private ConcurrentDictionary<Guid, int> counter = new();

        public ConcurrentDictionary<Guid, int> GetCounters() => counter;

        public bool IncrementCounter(Guid id)
        {
            if (counter.TryGetValue(id, out var currentCounter))
            {
                counter[id] = currentCounter + 1;
                return true;
            }

            return false;
        }

        public void AddNewCounter()
        {
            var id = Guid.NewGuid();

            counter[id] = 0;
        }
    }
}
