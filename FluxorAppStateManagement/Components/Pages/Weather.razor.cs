using FluxorAppStateManagement.State;
using FluxorAppStateManagement.State.Events.Update;
using FluxorAppStateManagement.State.State;
using Microsoft.AspNetCore.Components;

namespace FluxorAppStateManagement.Components.Pages
{
    public partial class Weather : IDisposable
    {
        [Inject] private StateManager StateManager { get; set; }

        private WeatherViewState weatherViewState { get; set; } = new();

        public void Dispose()
        {
            StateManager.StateChanged -= StateChanged;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            StateManager.StateChanged += StateChanged;
            StateManager.GetState(weatherViewState);;
        }

        private void StateChanged(object obj, EventArgs newStateActionEvent)
        {
            weatherViewState.ApplyNewState(newStateActionEvent);
            StateHasChanged();
        }

        private void UpdateWeather()
        {
            StateManager.UpdateState(new NewWeatherActionEvent());
        }
    }
}
