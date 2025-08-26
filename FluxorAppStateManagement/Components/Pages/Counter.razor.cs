using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State;
using FluxorAppStateManagement.State.Events.Update;
using FluxorAppStateManagement.State.State;
using Microsoft.AspNetCore.Components;

namespace FluxorAppStateManagement.Components.Pages
{
    public partial class Counter : IDisposable
    {
        [Inject]
        private StateManager StateManager { get; set; }

        private CounterViewState viewState = new();

        private Guid id;

        public void Dispose()
        {
            StateManager.StateChanged -= StateChangedAsync;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            StateManager.StateChanged += StateChangedAsync;
            StateManager.GetState(viewState); ;
        }

        private void IncrementCount()
        {
            StateManager.UpdateState(new IncrementCounterActionEvent() { CounterId = id });
        }

        private void AddNewCounter()
        {
            StateManager.UpdateState(new NewCounterActionEvent());
        }

        private async void StateChangedAsync(object obj, ReduceEventArgs newStateActionEvent)
        {
            newStateActionEvent.InvokeReducer(viewState);
            await InvokeAsync(StateHasChanged);
        }
    }
}
