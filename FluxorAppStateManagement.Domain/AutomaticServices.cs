using System.Timers;
using Timer = System.Timers.Timer;

namespace FluxorAppStateManagement.Domain
{
    public class AutomaticServices
    {
        private readonly CounterService counterService;
        private readonly WeatherService weatherService;
        private Timer timer;
        public AutomaticServices(CounterService counterService, WeatherService weatherService)
        {
            this.counterService = counterService;
            this.weatherService = weatherService;

            timer = new Timer();
            timer.AutoReset = true;
            timer.Interval = 10000;
            timer.Start();
            timer.Elapsed += TimerCallback;
        }

        private void TimerCallback(object? sender, ElapsedEventArgs e)
        {
            counterService.AddNewCounter();
            //weatherService.AddNewForecast();
        }
    }
}
