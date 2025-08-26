using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
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
            CounterService.GetCounters();
        }

        private void IncrementCount()
        {
            CounterService.IncrementCounter(id);
        }

        private void AddNewCounter()
        {
            CounterService.AddNewCounter();
        }

        private async void StateChangedAsync(object obj, ReduceEventArgs newStateActionEvent)
        {
            newStateActionEvent.InvokeReducer(viewState);
            await InvokeAsync(StateHasChanged);
        }
    }
}
