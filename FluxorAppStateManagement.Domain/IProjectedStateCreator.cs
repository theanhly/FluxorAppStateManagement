using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.Domain
{
    public interface IProjectedStateCreator
    {
        IReadOnlyList<IProjectedApplicationState> Create(NewCountEventArgs args);
        IReadOnlyList<IProjectedApplicationState> Create(NewCounterEventArgs args);
        IReadOnlyList<IProjectedApplicationState> Create(NewForecastEventArgs args);
        IReadOnlyList<IProjectedApplicationState> Create(ForecastsEventArgs args);
        IReadOnlyList<IProjectedApplicationState> Create(CounterEventArgs args);
        IReadOnlyList<IProjectedApplicationState> Create(CityForecastEventArgs args);
    }
}
