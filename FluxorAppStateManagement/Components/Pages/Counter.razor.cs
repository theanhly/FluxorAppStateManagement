using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.Domain.Services;
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

        [Inject]
        private EventBus.EventBus EventBus { get; set; }

        private CounterViewState viewState = new();

        private Guid id;

        public void Dispose()
        {
            EventBus.Unsubscribe<NewProjectedApplicationStateEventArgs>(StateChangedAsync);
            CounterService.CounterChanged -= GetState;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EventBus.Subscribe<NewProjectedApplicationStateEventArgs>(StateChangedAsync);
            CounterService.CounterChanged += GetState;
            StateManager.CreateProjectedApplicationStates(viewState);
        }

        private void GetState(object obj, ReduceEventArgs args)
        {
            StateManager.CreateProjectedApplicationStates(viewState, args);
        }

        private void IncrementCount()
        {
            CounterService.IncrementCounter(id);
        }

        private void AddNewCounter()
        {
            CounterService.AddNewCounter();
        }

        private async void StateChangedAsync(NewProjectedApplicationStateEventArgs newStateActionEvent)
        {
            if (newStateActionEvent.NewState is CounterViewState counterViewState)
            {
                viewState = counterViewState;
                await InvokeAsync(StateHasChanged);
            }
        }
    }
}
