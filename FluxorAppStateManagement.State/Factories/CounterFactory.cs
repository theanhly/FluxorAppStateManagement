using System.Collections.ObjectModel;
using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.State.Events.Notify;
using FluxorAppStateManagement.State.Events.Update;
using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State.Factories
{
    public class CounterFactory(CounterService counterService)
    {
        public NewCounterStateActionEvent GetState()
        {
            return new NewCounterStateActionEvent()
            {
                ApplicationStateTransition = _ => new CounterState()
                {
                    Counters = new ReadOnlyDictionary<Guid, int>(counterService.GetCounters())
                }
            };
        }

        public EventArgs UpdateState(ActionEvent actionEvent)
        {
            if (actionEvent is NewCounterActionEvent)
            {
                counterService.AddNewCounter();
                return new NewCounterStateActionEvent()
                {
                    ApplicationStateTransition = state => state with
                    {
                        Counters = new ReadOnlyDictionary<Guid, int>(counterService.GetCounters())
                    }
                };
            }
            else if (actionEvent is IncrementCounterActionEvent incrementCounterActionEvent)
            {
                var success = counterService.IncrementCounter(incrementCounterActionEvent.CounterId);

                if (success)
                {
                    return new NewCounterStateActionEvent()
                    {
                        ApplicationStateTransition = state => state with
                        {
                            Counters = new ReadOnlyDictionary<Guid, int>(counterService.GetCounters())
                        }
                    };
                }
            }

            return new();
        }
    }
}
