namespace FluxorAppStateManagement.Domain.Events
{
    public class ForecastsEventArgs : ReduceEventArgs
    {
        public IList<Weather> Forecasts { get; set; }
        public IList<Weather> RegionalForecasts { get; set; }

        public override IReadOnlyList<IProjectedApplicationState> InvokeStateCreator(IProjectedStateCreator creator)
        {
            return creator.Create(this);
        }
    }
}
