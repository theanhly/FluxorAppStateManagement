using System.Timers;
using Timer = System.Timers.Timer;

namespace FluxorAppStateManagement.Domain.Services
{
    public class AutomaticServices
    {
        private readonly CounterService counterService;
        private Timer timer;
        public AutomaticServices(CounterService counterService)
        {
            this.counterService = counterService;

            timer = new Timer();
            timer.AutoReset = true;
            timer.Interval = 10000;
            timer.Start();
            timer.Elapsed += TimerCallback;
        }

        private void TimerCallback(object? sender, ElapsedEventArgs e)
        {
            counterService.AddNewCounter();
        }
    }
}
