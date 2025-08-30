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
        [Inject] private CounterService CounterService { get; set; }
        [Inject] private EventBus.EventBus EventBus { get; set; }

        private WeatherViewState weatherViewState { get; set; } = new();
        private string city;

        public void Dispose()
        {
            EventBus.Unsubscribe<NewProjectedApplicationStateEventArgs>(StateChangedAsync);
            WeatherService.WeatherChanged -= GetState;
            CounterService.CounterChanged -= GetState;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EventBus.Subscribe<NewProjectedApplicationStateEventArgs>(StateChangedAsync);
            WeatherService.WeatherChanged += GetState;
            CounterService.CounterChanged += GetState;

            StateManager.CreateProjectedApplicationStates(weatherViewState);
        }

        private void GetState(object obj, ReduceEventArgs args)
        {
            StateManager.CreateProjectedApplicationStates(weatherViewState, args);
        }

        private async void StateChangedAsync(NewProjectedApplicationStateEventArgs newStateActionEvent)
        {
            if (newStateActionEvent.NewState is WeatherViewState weatherViewState)
            {
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
                WeatherService.UpdateForecast(city);
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
            WeatherService.GetForecasts(city);
        }

        private void AddCity()
        {
            WeatherService.AddCity(city);
        }
    }
}
