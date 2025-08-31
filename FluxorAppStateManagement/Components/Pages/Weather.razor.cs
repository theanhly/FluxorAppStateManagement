using FluxorAppStateManagement.Domain.Events;
using FluxorAppStateManagement.Domain.Services;
using FluxorAppStateManagement.State;
using FluxorAppStateManagement.State.State;
using Microsoft.AspNetCore.Components;

namespace FluxorAppStateManagement.Components.Pages
{
    public partial class Weather : IDisposable
    {
        [Inject] private StateManager StateManager { get; set; }
        [Inject] private WeatherService WeatherService { get; set; }
        [Inject] private EventBus.EventBus EventBus { get; set; }

        private WeatherViewState weatherViewState { get; set; } = new();
        private string city;
        private bool updatingForecasts = false;
        private bool loadingForecasts => weatherViewState.WeatherState.Forecasts == null || updatingForecasts;

        public void Dispose()
        {
            EventBus.Unsubscribe<NewProjectedApplicationStateEventArgs>(StateChangedAsync);
            EventBus.Unsubscribe<ReduceEventArgs>(GetState);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EventBus.Subscribe<NewProjectedApplicationStateEventArgs>(StateChangedAsync);
            EventBus.Subscribe<ReduceEventArgs>(GetState);

            StateManager.CreateProjectedApplicationStates(weatherViewState);
        }

        private void GetState(ReduceEventArgs args)
        {
            StateManager.CreateProjectedApplicationStates(weatherViewState, args);
        }

        private async void StateChangedAsync(NewProjectedApplicationStateEventArgs newStateActionEvent)
        {
            if (newStateActionEvent.NewState is WeatherViewState weatherViewState)
            {
                if (newStateActionEvent.EventArgs is NewForecastEventArgs && updatingForecasts)
                {
                    updatingForecasts = false;
                }
                this.weatherViewState = weatherViewState;
                await InvokeAsync(StateHasChanged);
            }
        }

        private void AddWeather()
        {
            if (!string.IsNullOrEmpty(city))
            {
                WeatherService.AddNewForecast(city);
            }
        }

        private void UpdateWeather()
        {
            if (!string.IsNullOrEmpty(city))
            {
                weatherViewState.WeatherState = weatherViewState.WeatherState with
                {
                    Forecasts = null
                };
                updatingForecasts = true;
                WeatherService.UpdateForecast(city);
            }
        }

        private void GetForecastsForCity(string city)
        {
            this.city = city;
            updatingForecasts = false;

            weatherViewState.WeatherState = weatherViewState.WeatherState with
            {
                RegionalForcasts = null,
                Forecasts = null
            };
            WeatherService.GetForecasts(city);
        }

        private void AddCity()
        {
            WeatherService.AddCity(city);
        }
    }
}
