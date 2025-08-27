namespace FluxorAppStateManagement.Domain.Events
{
    public class ForecastsEventArgs : ReduceEventArgs
    {
        public string City { get; set; }

        public override IProjectedApplicationState InvokeStateCreator(IProjectedStateCreator creator)
        {
            return creator.Create(this);
        }
    }
}
