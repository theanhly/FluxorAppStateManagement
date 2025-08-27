using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.State;
using FluxorAppStateManagement.State.State;
using Microsoft.AspNetCore.Components;

namespace FluxorAppStateManagement.Components.Pages
{
    public partial class Counter : IDisposable
    {
        [Inject]
        private StateManager StateManager { get; set; }

        [Inject]
        private CounterService CounterService { get; set; }

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
            StateManager.UpdateState(viewState, CounterService.GetCounters);
        }

        private void IncrementCount()
        {
            StateManager.UpdateState(viewState, () => CounterService.IncrementCounter(id));
        }

        private void AddNewCounter()
        {
            StateManager.UpdateState(viewState, CounterService.AddNewCounter);
        }

        private async void StateChangedAsync(object obj, NewProjectedApplicationStateEventArgs newStateActionEvent)
        {
            if (newStateActionEvent.NewState is CounterViewState counterViewState)
            {
                viewState = counterViewState;
                await InvokeAsync(StateHasChanged);
            }
        }
    }
}
