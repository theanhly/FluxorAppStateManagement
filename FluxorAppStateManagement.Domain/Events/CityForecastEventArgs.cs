namespace FluxorAppStateManagement.Domain.Events
{
    public class CityForecastEventArgs : ReduceEventArgs
    {
        public string City { get; set; }

        public override IReadOnlyList<IProjectedApplicationState> InvokeStateCreator(IProjectedStateCreator creator)
        {
            return creator.Create(this);
        }
    }
}
