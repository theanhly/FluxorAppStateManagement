namespace FluxorAppStateManagement.Domain.Events
{
    public class NewForecastEventArgs : ReduceEventArgs
    {
        public string City { get; init; }

        public override IProjectedApplicationState InvokeStateCreator(IProjectedStateCreator creator)
        {
            return creator.Create(this);
        }
    }
}
