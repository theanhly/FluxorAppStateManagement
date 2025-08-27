using FluxorAppStateManagement.Domain;
using FluxorAppStateManagement.State;
using FluxorAppStateManagement.State.State;
using Microsoft.AspNetCore.Components;

namespace FluxorAppStateManagement.Components.Pages
{
    public partial class Weather : IDisposable
    {
        [Inject] private StateManager StateManager { get; set; }
        [Inject] private WeatherService WeatherService { get; set; }

        private WeatherViewState weatherViewState { get; set; } = new();
        private string city;

        public void Dispose()
        {
            StateManager.StateChanged -= StateChangedAsync;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            StateManager.StateChanged += StateChangedAsync;

            StateManager.UpdateState(weatherViewState, () =>
            {
                WeatherService.GetCities();
            });
        }

        private async void StateChangedAsync(object obj, NewProjectedApplicationStateEventArgs newStateActionEvent)
        {
            if (newStateActionEvent.NewState is WeatherViewState weatherViewState)
            {
                this.weatherViewState = weatherViewState;
                await InvokeAsync(StateHasChanged);
            }
        }

        private void UpdateWeather()
        {
            if (!string.IsNullOrEmpty(city))
            {
                StateManager.UpdateState(weatherViewState, () =>
                {
                    WeatherService.AddNewForecast(city);
                });
            }
        }

        private void GetForecastsForCity(string city)
        {
            this.city = city;

            weatherViewState.WeatherState = weatherViewState.WeatherState with
            {
                RegionalForcasts = null, 
                Forecasts = null 
            }; 
            StateManager.UpdateState(weatherViewState, () =>
            {
                WeatherService.GetForecasts(city);
            });
        }

        private void AddCity()
        {
            StateManager.UpdateState(weatherViewState, () =>
            {
                WeatherService.AddCity(city);
            });
        }
    }
}
