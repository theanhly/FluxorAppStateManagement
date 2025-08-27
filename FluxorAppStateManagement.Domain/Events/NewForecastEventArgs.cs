namespace FluxorAppStateManagement.Domain.Events
{
    public class NewForecastEventArgs : ReduceEventArgs
    {
        public Weather Weather { get; init; }

        public override IReadOnlyList<IProjectedApplicationState> InvokeStateCreator(IProjectedStateCreator creator)
        {
            return creator.Create(this);
        }
    }
}
