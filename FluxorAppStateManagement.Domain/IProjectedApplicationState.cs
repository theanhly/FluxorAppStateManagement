using FluxorAppStateManagement.Domain.Events;

namespace FluxorAppStateManagement.Domain
{
    public interface IProjectedApplicationState
    {
        IReadOnlyList<Type> GetStates();

        void Reduce(NewCountEventArgs args);
        void Reduce(NewCounterEventArgs args);
        void Reduce(NewForecastEventArgs args);
        void Reduce(ForecastsEventArgs args);
        void Reduce(CounterEventArgs args);
    }
}
