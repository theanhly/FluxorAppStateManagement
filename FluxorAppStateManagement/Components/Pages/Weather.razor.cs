using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.State;
using FluxorAppStateManagement.State.State;
using Microsoft.AspNetCore.Components;

namespace FluxorAppStateManagement.Components.Pages
{
    public partial class Weather : IDisposable
    {
        [Inject] private StateManager StateManager { get; set; }
        [Inject] private CounterService CounterService { get; set; }
        [Inject] private WeatherService WeatherService { get; set; }

        private WeatherViewState weatherViewState { get; set; } = new();

        public void Dispose()
        {
            StateManager.StateChanged -= StateChangedAsync;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            StateManager.StateChanged += StateChangedAsync;
            CounterService.GetCounters();
            WeatherService.GetForecasts();
        }

        private async void StateChangedAsync(object obj, ReduceEventArgs newStateActionEvent)
        {
            newStateActionEvent.InvokeReducer(weatherViewState);
            await InvokeAsync(StateHasChanged);
        }

        private void UpdateWeather()
        {
            WeatherService.AddNewForecast();
        }
    }
}
