using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.Domain
{
    public interface IProjectedStateCreator
    {
        IProjectedApplicationState Create(NewCountEventArgs args);
        IProjectedApplicationState Create(NewCounterEventArgs args);
        IProjectedApplicationState Create(NewForecastEventArgs args);
        IProjectedApplicationState Create(ForecastsEventArgs args);
        IProjectedApplicationState Create(CounterEventArgs args);
        IProjectedApplicationState Create(CityForecastEventArgs args);
    }
}
