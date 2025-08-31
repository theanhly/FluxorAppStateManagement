using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.Domain.Services
{
    public class CounterService(CounterBackend backend, EventBus.EventBus eventBus)
    {
        
        public void GetCounters()
        {
            eventBus.Publish<ReduceEventArgs>(new CounterEventArgs());
        }

        public bool IncrementCounter(Guid id)
        {
            if (backend.IncrementCounter(id))
            {
                eventBus.Publish<ReduceEventArgs>(new NewCountEventArgs());
                return true;
            }

            return false;
        }

        public void AddNewCounter()
        {
            backend.AddNewCounter();
            eventBus.Publish<ReduceEventArgs>(new NewCounterEventArgs());
        }
    }
}
