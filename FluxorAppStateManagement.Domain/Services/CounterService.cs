using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.Domain.Services
{
    public class CounterService(CounterBackend backend)
    {
        public event EventHandler<ReduceEventArgs> CounterChanged;
        
        public void GetCounters()
        {
            CounterChanged?.Invoke(this, new CounterEventArgs());
        }

        public bool IncrementCounter(Guid id)
        {
            if (backend.IncrementCounter(id))
            {
                CounterChanged?.Invoke(this, new NewCountEventArgs());
                return true;
            }

            return false;
        }

        public void AddNewCounter()
        {
            backend.AddNewCounter();
            CounterChanged?.Invoke(this, new NewCounterEventArgs());
        }
    }
}
