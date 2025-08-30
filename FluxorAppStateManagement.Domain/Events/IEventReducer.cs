namespace FluxorAppStateManagement.Domain.Events
{
    public interface IEventReducer
    {
        void Create();
        void Create(NewCountEventArgs args);
        void Create(NewCounterEventArgs args);
        void Create(NewForecastEventArgs args);
        void Create(ForecastsEventArgs args);
        void Create(CounterEventArgs args);
        void Create(CityForecastEventArgs args);
    }
}
