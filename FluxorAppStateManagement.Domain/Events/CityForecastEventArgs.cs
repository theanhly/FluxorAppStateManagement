namespace FluxorAppStateManagement.Domain.Events
{
    public class CityForecastEventArgs : ReduceEventArgs
    {
        public IList<string> Cities { get; set; }

        public override IReadOnlyList<IProjectedApplicationState> InvokeStateCreator(IProjectedStateCreator creator)
        {
            return creator.Create(this);
        }
    }
}
