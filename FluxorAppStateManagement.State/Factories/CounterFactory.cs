using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.State.Events.Update;

namespace FluxorAppStateManagement.State.Factories
{
    public class CounterFactory
    {
        private CounterService counterService;

        public CounterFactory(CounterService counterService)
        {
            this.counterService = counterService;
        }

        public void GetState()
        {
            counterService.GetCounters();
        }

        public void UpdateState(ActionEvent actionEvent)
        {
            if (actionEvent is NewCounterActionEvent)
            {
                counterService.AddNewCounter();
            }
            else if (actionEvent is IncrementCounterActionEvent incrementCounterActionEvent)
            {
                counterService.IncrementCounter(incrementCounterActionEvent.CounterId);
            }
        }
    }
}
